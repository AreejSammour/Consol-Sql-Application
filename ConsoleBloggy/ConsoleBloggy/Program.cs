using ConsoleBloggy;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

string Connectionstring = "Server= ;Database=BloggySQL;Trusted_Connection=True";

SqlConnection Connection = new SqlConnection(Connectionstring);

Console.WriteLine("Welcome to your blogging app");
Console.WriteLine("============================");
Console.WriteLine();

bool Loop = true;

while (Loop)
{
    Console.WriteLine("Choose one of the following options: ");
    Console.WriteLine("   1. Display all posts.");
    Console.WriteLine("   2. Display the names of all categories.");
    Console.WriteLine("   3. Add a new blog post.");
    Console.WriteLine("   4. Add a new category.");
    Console.WriteLine("   5. Display all the blog titles from a specific category.");
    Console.WriteLine("   6. Add an existing blog post to an existing category.");
    Console.WriteLine("To quit enter 'q'.");
    Console.Write("Your choice: ");
    string UserChoice = Console.ReadLine();

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;

    if (UserChoice == "1") //Display all posts.
    {
        Console.WriteLine("Posts are As follow: ");
        Console.WriteLine();
        string Sql1 = "SELECT * FROM BlogPost";
        SqlCommand command1 = new SqlCommand(Sql1, Connection);
        Connection.Open();
        SqlDataReader Result1 = command1.ExecuteReader();

        while (Result1.Read())
        {
            string BlogId = Result1.GetValue(0).ToString();
            string BlogTitle = Result1.GetValue(1).ToString();
            string BlogText = Result1.GetValue(2).ToString();

            Console.WriteLine(BlogId + ") " + BlogTitle);
            Console.WriteLine("   " + BlogText);
            Console.WriteLine("-------------------");
            Console.WriteLine();
        }
        Connection.Close();
    }
    else if (UserChoice == "2") //Display the names of all categories.
    {
        Console.WriteLine("The names of all categories are: ");
        Console.WriteLine();
        string Sql2 = "SELECT * FROM Category";
        SqlCommand command2 = new SqlCommand(Sql2, Connection);
        Connection.Open();
        SqlDataReader Result2 = command2.ExecuteReader();

        while (Result2.Read())
        {
            string CategoryId = Result2.GetValue(0).ToString();
            string CategoryName = Result2.GetValue(1).ToString();

            Console.WriteLine(CategoryId + ") " + CategoryName.PadRight(50));
        }
        Connection.Close();
        Console.WriteLine("-------------------");
        Console.WriteLine();
    }
    else if (UserChoice == "3") //Add a new blog post.
    {
        Console.Write("Enter the title of the new post: ");
        string PostTitle = Console.ReadLine();
        Console.WriteLine("Enter the text of the post (max 2000 chars): ");
        string PostText = Console.ReadLine();

        string Sql3 = "INSERT INTO BlogPost VALUES( '" + PostTitle + "' , '" + PostText + "' )";
        SqlCommand command3 = new SqlCommand(Sql3, Connection);
        
        Connection.Open();
        command3.ExecuteNonQuery();
        Console.WriteLine(">>> It has been added.");
        Console.WriteLine("====================");
        Console.WriteLine();
        Connection.Close();
    }
    else if (UserChoice == "4")  //Add a new category.
    {
        Console.Write("Enter the name of the new Category: ");
        string CatName = Console.ReadLine();

        string Sql4 = "INSERT INTO Category VALUES( '" + CatName + "' )";
        SqlCommand command4 = new SqlCommand(Sql4, Connection);

        Connection.Open();
        command4.ExecuteNonQuery();
        Console.WriteLine(">>> It has been added.");
        Console.WriteLine("====================");
        Console.WriteLine();
        Connection.Close();
    }
    else if (UserChoice == "5")  //Display all the blog titles from a specific category.
    {
        Console.Write("Enter the required Category to display: ");
        FindCategory GetCategory = new FindCategory();
        GetCategory.NameToFind = Console.ReadLine();
        GetCategory.Connect = Connection;
        GetCategory.FindCat(); // Get Category Id in DB

        if (GetCategory.IdResult != 0)
        {
            FindCatBlog BlogList = new FindCatBlog();
            BlogList.IdResult = GetCategory.IdResult;
            BlogList.Connect = Connection;
            BlogList.FindCBId(); // Get Blog Id in DB
            
            if (BlogList.y == 0)
            {
                Console.WriteLine();
                Console.WriteLine("  >>> There is no Titles in this Category!");
            }    
            else
            {
                Console.WriteLine();
                Console.WriteLine(">>> The existing Titles are as follows: ");
                int L = 0;
                foreach ( int z in BlogList.IdToGet)
                {
                    L++;
                    Blog FindBlogTitle = new Blog();
                    FindBlogTitle.y = z;
                    FindBlogTitle.x = L;
                    FindBlogTitle.Connect = Connection;
                    FindBlogTitle.FindBlogTitle(); // Get Blog titles in DB
                }
            }   
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine(">>> This category does not exist");
        }
        Console.WriteLine("-------------------");
        Console.WriteLine();
    }
    else if (UserChoice == "6")  //Add an existing blog post to an existing category.
    {
        Console.WriteLine("Enter The Blog post title: ");
        string Title = Console.ReadLine();
        string Sql8 = "SELECT Id FROM BlogPost WHERE BlogTitle = '" + Title + "'"; // Get Blog Id in DB
        SqlCommand command8 = new SqlCommand(Sql8, Connection);
        Connection.Open();
        SqlDataReader TitleToFind = command8.ExecuteReader();
        int TitleId = 0;
        while (TitleToFind.Read())
        {
            TitleId = (int)TitleToFind.GetValue(0);
        }
        Connection.Close();

        if (TitleId != 0)
        {
            Console.WriteLine("Choose the Category name to add Blog post to: "); // Display Names of Categories available to choose from
            string Sql2 = "SELECT * FROM Category";
            SqlCommand command2 = new SqlCommand(Sql2, Connection);
            Connection.Open();
            SqlDataReader Result2 = command2.ExecuteReader();
            while (Result2.Read())
            {
                string CategoryName = Result2.GetValue(1).ToString();
                Console.WriteLine("   " + CategoryName.PadRight(50));
            }
            Connection.Close();
            Console.Write("Enter Category Name: ");

            FindCategory GetCategory = new FindCategory();
            GetCategory.NameToFind = Console.ReadLine();
            GetCategory.Connect = Connection;
            GetCategory.FindCat(); // Get Category Id in DB

            if (GetCategory.IdResult != 0)
            {
                string Sql10 = "INSERT INTO CategoryBlog VALUES( '" + GetCategory.IdResult + "' , '" + TitleId + "' )";
                SqlCommand command10 = new SqlCommand(Sql10, Connection);

                Connection.Open();
                command10.ExecuteNonQuery();
                Console.WriteLine(">>> It has been added.");
                Connection.Close();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(">>> This category does not exist");
                Console.WriteLine("    Please add the new category first.");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine(">>> This Title does not exist"); 
        }
        
        Console.WriteLine("====================");
        Console.WriteLine();
    }
    else if (UserChoice == "q" || UserChoice == "Q")
    {
        Loop = false;
    }

    Console.ResetColor();
}

// Present the list when exit

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine();
Console.WriteLine("We are waiting for you next time to browse the following: ");
Console.WriteLine();

// Make list for Categories in App
CategoryList Categories = new CategoryList();
Categories.connect = Connection;
Categories.FindCats();
int n = 0;

foreach (var item in Categories.GetCatName) // For each category get BLog posts titles
{
    Console.WriteLine((n+1) + ") " + item);

    FindCatBlog BlogList = new FindCatBlog();
    BlogList.IdResult = Categories.GetCatId[n];
    BlogList.Connect = Connection;
    BlogList.FindCBId(); // Get Blog Id in DB

    if (BlogList.y == 0)
    {
        Console.WriteLine("  >>> There are no titles here yet!");
    }
    else
    {
        int L = 0;
        foreach (int z in BlogList.IdToGet)
        {
            L++;
            Blog FindBlogTitle = new Blog();
            FindBlogTitle.y = z;
            FindBlogTitle.x = L;
            FindBlogTitle.Connect = Connection;
            FindBlogTitle.FindBlogTitle(); // Get Blog titles in DB
        }

    }
    Console.WriteLine("-------------------");
    n++;
}
Console.ResetColor();






