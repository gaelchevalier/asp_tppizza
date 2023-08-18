using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BO;
using System;
using TPPizza.ViewModels;
using Humanizer;

namespace TPPizza.Controllers
{
    public class PizzaController : Controller
    {
        private static IList<Pizza> _PizzasMock = GetPizzasMock();

        // GET: PizzaController
        public ActionResult Index()
        {
            IList<Pizza> pizzas = _PizzasMock;
            return View(pizzas);
        }

        // GET: PizzaController/Details/5
        public ActionResult Details(int id)
        {
            Pizza? pizza = GetPizza(id);
            if (pizza == null)
            {
                return View(nameof(Index));
            }
            return View(pizza);
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            PizzaViewData pizzaVM = new PizzaViewData();
            pizzaVM.Pates = PatesDisponibles;
            pizzaVM.Ingredients = IngredientsDisponibles;
            return View(pizzaVM);
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaViewData pizzaData)
        {
            try
            {
                int newPizzaId = _PizzasMock.Count + 1;
                var pateSelected = PatesDisponibles.Single(x => x.Id == pizzaData.Pate.Id);
                List<int> selectedIngredientsIds = pizzaData.SelectedIngredients;
                List<Ingredient> selectedIngredients = IngredientsDisponibles.Where(ing => selectedIngredientsIds.Contains(ing.Id)).ToList();
                Pizza newPizza = new Pizza { Id = newPizzaId, Nom = pizzaData.Nom, Pate = pateSelected, Ingredients = selectedIngredients };
                _PizzasMock.Add(newPizza);
                return RedirectToAction(nameof(Index), _PizzasMock);
            }
            catch
            {
                return View(nameof(Index), _PizzasMock);
            }
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza? pizza = GetPizza(id);
            List<int> selectedIngredientsId = pizza.Ingredients.Select(x => x.Id).ToList();
            PizzaViewData pizzaVM = new PizzaViewData { 
                Nom=pizza.Nom, 
                Pate=pizza.Pate, 
                SelectedIngredients= selectedIngredientsId,
                Pates=PatesDisponibles, 
                Ingredients=IngredientsDisponibles
            };

            if (pizza == null)
            {
                return View(nameof(Index));
            }
            return View(pizzaVM);
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, PizzaViewData pizzaData)
        {
            try
            {
                Pizza? pizzaToEdit = GetPizza(id);

                if (pizzaToEdit == null)
                {
                    return View(nameof(Index));
                }
                pizzaToEdit.Nom = pizzaData.Nom;
                pizzaToEdit.Pate = PatesDisponibles.Single(x => x.Id == pizzaData.Pate.Id);
                pizzaToEdit.Ingredients = IngredientsDisponibles.Where(ing => pizzaData.SelectedIngredients.Contains(ing.Id)).ToList();
                return RedirectToAction(nameof(Index), _PizzasMock);
            }
            catch
            {
                return View(nameof(Index), _PizzasMock);
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza? pizza = GetPizza(id);
            if (pizza == null)
            {
                return View(nameof(Index));
            }
            return View(pizza);
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Pizza? pizzaToDelete = GetPizza(id);
                if (pizzaToDelete == null) { return NotFound(); };
                _PizzasMock.Remove(pizzaToDelete);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index), _PizzasMock);
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

        private static List<Pizza> GetPizzasMock()
        {
            return new List<Pizza>
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

        public static Pizza? GetPizza(int id)
        {
            return _PizzasMock.SingleOrDefault(x => x.Id == id);
        }
    }
}
