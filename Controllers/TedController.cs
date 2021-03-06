﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TedBank.Models;

namespace TedBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TedController : ControllerBase
    {
        private readonly TedBankContext _context;

        public TedController(TedBankContext context)
        {
            _context = context;
        }

        // GET: api/Ted
        [HttpGet]
        public IEnumerable<TedItem> GetTedItem()
        {
            return _context.TedItem;
        }

        // GET: api/Ted/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTedItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tedItem = await _context.TedItem.FindAsync(id);

            if (tedItem == null)
            {
                return NotFound();
            }

            return Ok(tedItem);
        }

        // PUT: api/Ted/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTedItem([FromRoute] int id, [FromBody] TedItem tedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tedItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(tedItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TedItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ted
        [HttpPost]
        public async Task<IActionResult> PostTedItem([FromBody] TedItem tedItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TedItem.Add(tedItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                int a = 0;
            }
            return CreatedAtAction("GetTedItem", new { id = tedItem.Id }, tedItem);
        }

        // DELETE: api/Ted/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTedItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tedItem = await _context.TedItem.FindAsync(id);
            if (tedItem == null)
            {
                return NotFound();
            }

            _context.TedItem.Remove(tedItem);
            await _context.SaveChangesAsync();

            return Ok(tedItem);
        }

        private bool TedItemExists(int id)
        {
            return _context.TedItem.Any(e => e.Id == id);
        }
        // GET: api/Ted/Tags
        [Route("topics")]
        [HttpGet]
        public async Task<List<string>> GetTopics()
        {
            var tedTalks = (from t in _context.TedItem
                            select t.Topic).Distinct();

            var returned = await tedTalks.ToListAsync();

            return returned;
        }


        // GET: api/Meme/topic

        [HttpGet]
        [Route("topic")]
        public async Task<List<TedItem>> GetTopicsItem([FromQuery] string topics)
        {
            var tedTalks = from t in _context.TedItem
                           select t; //get all the ted talks


            if (!String.IsNullOrEmpty(topics)) //make sure user gave a topic to search
            {
                tedTalks = tedTalks.Where(s => s.Topic.ToLower().Equals(topics.ToLower())); // find the entries with the search topic and reassign
            }

            var returned = await tedTalks.ToListAsync(); //return the memes

            return returned;
        }
    }
}