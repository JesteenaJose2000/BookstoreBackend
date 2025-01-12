﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookStore.Models;

namespace BookStore.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class WishlistController : ApiController
    {
        private IWishlistRepository repository;
        public WishlistController()
        {
            repository = new WishlistSqlImpl();
        }
        [HttpGet]
        public IHttpActionResult Get(int UserId)
        {
            var data = repository.GetWishlistBooks(UserId);
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult Add(Wishlist wishlist)
        {
            var data = repository.AddWishlist(wishlist);
            return Ok(data);
        }
        [HttpDelete]
        public IHttpActionResult Delete(int WishlistId)
        {
            repository.DeleteWishlist(WishlistId);
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Update(int WishlistId, Wishlist wishlist)
        {
            repository.UpdateWishlist(WishlistId,wishlist);
            return Ok();
        }
    }
}
