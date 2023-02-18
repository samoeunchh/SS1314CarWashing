﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SS1314CarWashing.Models;

namespace SS1314CarWashing.Controllers;

public class SalesController : Controller
{
    private readonly AppDbContext _context;

    public SalesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Sales
    public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Sale.Include(s => s.Customer);
        return View(await appDbContext.ToListAsync());
    }
    public async Task<JsonResult> GetProductByCategory(Guid Id)
        => Json(await _context.Item.Where(x => x.ItemTypeId.Equals(Id)).ToListAsync());
    // GET: Sales/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null || _context.Sale == null)
        {
            return NotFound();
        }

        var sale = await _context.Sale
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(m => m.SaleId == id);
        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

    // GET: Sales/Create
    public async Task<IActionResult> Create()
    {
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName");
        ViewData["ItemTypes"] = await _context.ItemType.ToListAsync();
        return View();
    }
    public async Task<IActionResult> Print(Guid Id)
    {
        var saleDetails =await (from sd in _context.SaleDetail
                                join i in _context.Item on sd.ItemId equals i.ItemId
                           where sd.SaleId.Equals(Id)
                           select new SaleDetailDTO
                           {
                               ItemName=i.ItemName,
                               SaleDetailId=sd.SaleDetailId,
                               SaleId=sd.SaleId,
                               Amount=sd.Amount,
                               Price=sd.Price,
                               Qty=sd.Qty
                           }).ToListAsync();
        var sale = await (from s in _context.Sale
                          join c in _context.Customer
                          on s.CustomerId equals c.CustomerId
                          where s.SaleId == Id
                          select new SaleDTO
                          {
                              SaleId = s.SaleId,
                              CustomerName = c.CustomerName,
                              Total = s.Total,
                              GrandTotal = s.GrandTotal,
                              Discount = s.Discount,
                              IssueDate = s.IssueDate,
                              InvoiceNumber = s.InvoiceNumber,
                              SaleDetails = saleDetails
                          }).FirstOrDefaultAsync();
        return View(sale);
    }
    // POST: Sales/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Sale sale)
    {
        if (ModelState.IsValid)
        {
            var id = Guid.NewGuid();
            if(sale.SaleDetails is not null)
            {
                for(int i = 0; i < sale.SaleDetails.Count; i++)
                {
                    sale.SaleDetails[i].SaleId=id;
                    sale.SaleDetails[i].SaleDetailId = Guid.NewGuid();
                    var item = sale.SaleDetails[i];
                    //Reduce Stock
                    var product = _context.Item.Where(x => x.ItemId == item.ItemId && x.IsStock==true).FirstOrDefault();
                    if(product is not null)
                    {
                        _context.Item.Attach(product);
                        product.Qty -= item.Qty;
                    }
                }
            }
            sale.SaleId = id;
            sale.InvoiceNumber = DateTime.Now.ToString("yyyyMMddHHmmssff");
            _context.Add(sale);
            await _context.SaveChangesAsync();
            return Ok(id);
        }
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "PhoneNumber", sale.CustomerId);
        ViewData["ItemTypes"] = await _context.ItemType.ToListAsync();
        return View(sale);
    }

    // GET: Sales/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null || _context.Sale == null)
        {
            return NotFound();
        }

        var sale = await _context.Sale.FindAsync(id);
        if (sale == null)
        {
            return NotFound();
        }
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "PhoneNumber", sale.CustomerId);
        return View(sale);
    }

    // POST: Sales/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("SaleId,CustomerId,IssueDate,InvoiceNumber,Total,Discount,GrandTotal")] Sale sale)
    {
        if (id != sale.SaleId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(sale.SaleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "PhoneNumber", sale.CustomerId);
        return View(sale);
    }

    // GET: Sales/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null || _context.Sale == null)
        {
            return NotFound();
        }

        var sale = await _context.Sale
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(m => m.SaleId == id);
        if (sale == null)
        {
            return NotFound();
        }

        return View(sale);
    }

    // POST: Sales/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (_context.Sale == null)
        {
            return Problem("Entity set 'AppDbContext.Sale'  is null.");
        }
        var sale = await _context.Sale.FindAsync(id);
        if (sale != null)
        {
            _context.Sale.Remove(sale);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SaleExists(Guid id)
    {
      return _context.Sale.Any(e => e.SaleId == id);
    }
}
