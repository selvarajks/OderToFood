using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
       IEnumerable <Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Delete(int id);
        int GetCountOfRestaurants();
        int Commit();
    }

    public class InMemoreyRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoreyRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant{Id =1, Name ="Scotts Pizza",Location ="MaryLand" ,Cuisine = CuisineType.Italian },
                new Restaurant{Id =2, Name ="Cinnamon club",Location ="London", Cuisine = CuisineType.Mexican},
                new Restaurant{Id =3, Name ="PonnuSamy",Location ="California",Cuisine = CuisineType.Indian}

             };

        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);

        }

        

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault((r => r.Id == updatedRestaurant.Id));
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name =null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
                                  
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
