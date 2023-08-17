using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BO;

namespace TPPizza.Controllers
{
    public class PizzaController : Controller
    {
        // GET: PizzaController
        public ActionResult Index()
        {
            IList<Pizza> pizzas = PizzasMock;
            return View(pizzas);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // A mettre dans le controlleur pizza

        public static List<Ingredient> IngredientsDisponibles => new List<Ingredient> {
                    new Ingredient{Id=1,Nom="Mozzarella"},
                    new Ingredient{Id=2,Nom="Jambon"},
                    new Ingredient{Id=3,Nom="Tomate"},
                    new Ingredient{Id=4,Nom="Oignon"},
                    new Ingredient{Id=5,Nom="Cheddar"},
                    new Ingredient{Id=6,Nom="Saumon"},
                    new Ingredient{Id=7,Nom="Champignon"},
                    new Ingredient{Id=8,Nom="Poulet"}
                };

        public static List<Pate> PatesDisponibles => new List<Pate> {
                    new Pate{ Id=1,Nom="Pate fine, base crême"},
                    new Pate{ Id=2,Nom="Pate fine, base tomate"},
                    new Pate{ Id=3,Nom="Pate épaisse, base crême"},
                    new Pate{ Id=4,Nom="Pate épaisse, base tomate"}
                };

        public static List<Pizza> PizzasMock => new List<Pizza>
        {
            new Pizza{
                    Id=1, 
                    Nom="Margarita", 
                    Pate=PatesDisponibles.Single(x => x.Id==1), 
                    Ingredients=new List<Ingredient>{ IngredientsDisponibles.Single(x => x.Id==1) }
            },
            new Pizza{
                    Id=2,
                    Nom="Champi",
                    Pate=PatesDisponibles.Single(x => x.Id==2),
                    Ingredients=new List<Ingredient>{ 
                        IngredientsDisponibles.Single(x => x.Id==1),
                        IngredientsDisponibles.Single(x => x.Id==7)
                    }
            },
        };
    }
}
