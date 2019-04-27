using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data;
using OdeToFood.Core;
namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        public Restaurant Restaurant { get; set; }
        private readonly IRestaurantData restaurantData;
        public DeleteModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);

            if (Restaurant ==null)
            {

                return RedirectToPage("./NotFound");
            }
            return Page();

        }

        public IActionResult OnPost(int restaurantId)
        {
            var restaurant = restaurantData.Delete(restaurantId);
            restaurantData.Commit();
            if (restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{restaurant.Name} deelted";

            return RedirectToPage("./List");
        }
    }
}