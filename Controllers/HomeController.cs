using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NewsWorld.Data;
using NewsWorld.Models;
using System.Diagnostics;
using System.Data.SQLite;


namespace NewsWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            string connectionString = "Data Source=stories.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM stories";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["header"]}, Value: {reader["link"]}");
                            News news = new News
                            {
                                type = reader["category"] as string,
                                header = reader["header"] as string,
                                link = reader["link"] as string,
                                lat = reader["lat"] is DBNull ? null : (float?)Convert.ToSingle(reader["lat"]),
                                lng = reader["long"] is DBNull ? null : (float?)Convert.ToSingle(reader["long"]),
                                img = reader["media_link"] as string,
                                summary = reader["summary"] as string
                            };

                            _context.News.Add(news);
                        }
                        //_context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Map");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Map()
        {
            /*var applicationDbcontext = _context.News.Include(t => t.Id);*/

            var newsData = await _context.News.ToListAsync();

            return View(newsData);
        }
    }
}