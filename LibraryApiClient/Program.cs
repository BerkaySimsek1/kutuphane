using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibraryApiClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:5098/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Library API Client");
            Console.WriteLine("1. List Books");
            Console.WriteLine("2. Search Book by Title");
            Console.WriteLine("3. Add New Book");
            Console.WriteLine("4. Add New Author");
            Console.WriteLine("5. Add New Category");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ListBooksAsync();
                    break;
                case "2":
                    Console.Write("Enter book title to search: ");
                    var title = Console.ReadLine();
                    await SearchBookByTitleAsync(title);
                    break;
                case "3":
                    await AddNewBookAsync();
                    break;
                case "4":
                    await AddNewAuthorAsync();
                    break;
                case "5":
                    await AddNewCategoryAsync();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static async Task ListBooksAsync()
        {
            HttpResponseMessage response = await client.GetAsync("books");
            if (response.IsSuccessStatusCode)
            {
                var books = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Books: ");
                Console.WriteLine(books);
            }
            else
            {
                Console.WriteLine("Failed to retrieve books.");
            }
        }

        static async Task SearchBookByTitleAsync(string title)
        {
            HttpResponseMessage response = await client.GetAsync($"books/search?query={title}");
            if (response.IsSuccessStatusCode)
            {
                var books = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Search Results: ");
                Console.WriteLine(books);
            }
            else
            {
                Console.WriteLine("No books found matching the title.");
            }
        }

        static async Task AddNewBookAsync()
        {
            try
            {
                Console.Write("Enter title: ");
                var title = Console.ReadLine();
                Console.Write("Enter author ID: ");
                var authorId = Console.ReadLine();
                Console.Write("Enter category ID: ");
                var categoryId = Console.ReadLine();

                var book = new
                {
                    Title = title,
                    AuthorId = int.Parse(authorId),
                    CategoryId = int.Parse(categoryId),
                    Metadata = "{}"  // Varsayılan olarak boş bir JSON
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("books", book);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Book added successfully.");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Failed to add the book.");
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Error: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        static async Task AddNewAuthorAsync()
        {
            try
            {
                Console.Write("Enter author name: ");
                var name = Console.ReadLine();
                Console.Write("Enter author surname: ");
                var surname = Console.ReadLine();

                var author = new
                {
                    Name = name,
                    Surname = surname
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("authors", author);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Author added successfully.");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Failed to add the author.");
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Error: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task AddNewCategoryAsync()
        {
            try
            {
                Console.Write("Enter category name: ");
                var name = Console.ReadLine();

                var category = new
                {
                    Name = name
                };

                HttpResponseMessage response = await client.PostAsJsonAsync("categories", category);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Category added successfully.");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Failed to add the category.");
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Error: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
