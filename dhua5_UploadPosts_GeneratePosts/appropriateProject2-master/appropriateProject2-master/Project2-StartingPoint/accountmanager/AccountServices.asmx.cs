using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

//we need these to talk to mysql
using MySql.Data;
using MySql.Data.MySqlClient;
//and we need this to manipulate data from a db
using System.Data;

namespace accountmanager
{    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class AccountServices : System.Web.Services.WebService
    {
        //uploads post information into the database
        [WebMethod(EnableSession = true)]
        public void CreatePost(string postTitle, string postText, string actionableItem, string postUsername)
        {
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            string sqlSelect = "insert into posts(postTitle, postText, actionableItem, postUsername) " +
                "values(@postTitle, @postText, @actionableItem, @postUsername);";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@postTitle", HttpUtility.UrlDecode(postTitle));
            sqlCommand.Parameters.AddWithValue("@postText", HttpUtility.UrlDecode(postText));
            sqlCommand.Parameters.AddWithValue("@actionableItem", HttpUtility.UrlDecode(actionableItem));
            sqlCommand.Parameters.AddWithValue("@postUsername", HttpUtility.UrlDecode(postUsername));

            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            DataTable sqlDt = new DataTable();
            sqlDa.Fill(sqlDt);
        }

        [WebMethod(EnableSession = true)]
        public List<Post> GeneratePost()
        {
            //access the database and queries every post
            DataTable sqlDt = new DataTable("posts");
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            string sqlSelectStatement = "SELECT postID, postTitle, postText, actionableItem, postUsername, postDateTime, isSensored from posts";
            MySqlConnection sqlConnectionPosts = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommandPosts = new MySqlCommand(sqlSelectStatement, sqlConnectionPosts);
            MySqlDataAdapter sqlPosts = new MySqlDataAdapter(sqlCommandPosts);
            sqlPosts.Fill(sqlDt);

            //adds all the posts to a list
            List<Post> postList = new List<Post>();
            for (int i = 0; i < sqlDt.Rows.Count; i++)
            {
                postList.Add(new Post
                {
                    PostID = Convert.ToInt32(sqlDt.Rows[i]["postID"]),
                    PostTitle = sqlDt.Rows[i]["postTitle"].ToString(),
                    PostText = sqlDt.Rows[i]["postText"].ToString(),
                    ActionableItem = sqlDt.Rows[i]["actionableItem"].ToString(),
                    PostUsername = sqlDt.Rows[i]["postUsername"].ToString(),
                    PostDateTime = sqlDt.Rows[i]["postDateTime"].ToString(),
                    IsSensored = sqlDt.Rows[i]["isSensored"].ToString(),
                });
            }

            return postList;
        }


















































        [WebMethod(EnableSession = true)]
        public bool UploadTheirQuote(string quote, string firstName, string LastName, string rating, string genre1, string genre2, string genre3)
        {
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

            //string sqlSelect = "insert into account(firstName, lastName, email, password, firstFaveGenre, secondFaveGenre, activeAccount, adminAbility) " +
            //"values(@fnameValue, @lnameValue, @emailValue, @passwordValue, @genre1Value, @genre2Value, 0, 0); SELECT LAST_INSERT_ID();";
            string sqlSelect = "insert into quotes2(quoteText, qFirstName, qLastName, rating, genre1, genre2, genre3, approved, totalPoints, numRaters) " +
                "values(@quoteVal, @firstNameVal, @lastNameVal, @ratingVal, @genre1Val, @genre2Val, @genre3Val, '1', @ratingVal, '1');";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@quoteVal", HttpUtility.UrlDecode(quote));
            sqlCommand.Parameters.AddWithValue("@firstNameVal", HttpUtility.UrlDecode(firstName));
            sqlCommand.Parameters.AddWithValue("@lastNameVal", HttpUtility.UrlDecode(LastName));
            sqlCommand.Parameters.AddWithValue("@ratingVal", HttpUtility.UrlDecode(rating));
            sqlCommand.Parameters.AddWithValue("@genre1Val", HttpUtility.UrlDecode(genre1));
            sqlCommand.Parameters.AddWithValue("@genre2Val", HttpUtility.UrlDecode(genre2));
            sqlCommand.Parameters.AddWithValue("@genre3Val", HttpUtility.UrlDecode(genre3));

            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            DataTable sqlDt = new DataTable();
            sqlDa.Fill(sqlDt);
            return true;

            //sqlConnection.Open();
            ////we're using a try/catch so that if the query errors out we can handle it gracefully
            ////by closing the connection and moving on
            //try
            //{
            //    //sqlCommand.ExecuteNonQuery();
            //    //int accountID = Convert.ToInt32(sqlCommand.ExecuteScalar());
            //    //here, you could use this accountID for additional queries regarding
            //    //the requested account.  Really this is just an example to show you
            //    //a query where you get the primary key of the inserted row back from
            //    //the database!

            //    sqlConnection.Close();
            //    return true;
            //}
            //catch (Exception e)
            //{

            //}
            //sqlConnection.Close();
            //return false;


        }

        [WebMethod(EnableSession = true)]
        public bool LogOff()
        {
            //if they log off, then we remove the session.  That way, if they access
            //again later they have to log back on in order for their ID to be back
            //in the session!
            Session.Abandon();
            return true;
        }

        //EXAMPLE OF AN INSERT QUERY WITH PARAMS PASSED IN.  BONUS GETTING THE INSERTED ID FROM THE DB!
        [WebMethod(EnableSession = true)]
        public void RequestAccount(string firstName, string lastName, string email, string password, string firstFaveGenre, string secondFaveGenre)
        {
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //the only thing fancy about this query is SELECT LAST_INSERT_ID() at the end.  All that
            //does is tell mySql server to return the primary key of the last inserted row.

            //INSERT INTO `account` (`accountID`, `firstName`, `lastName`, `email`, `password`, `firstFaveGenre`, `secondFaveGenre`,
            //`activeAccount`, `AdminAbility`) VALUES (NULL, '', '', '', '', NULL, NULL, '0', '0')
            string sqlSelect = "insert into account(firstName, lastName, email, password, firstFaveGenre, secondFaveGenre, activeAccount, adminAbility) " +
                "values(@fnameValue, @lnameValue, @emailValue, @passwordValue, @genre1Value, @genre2Value, 0, 0); SELECT LAST_INSERT_ID();";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@fnameValue", HttpUtility.UrlDecode(firstName));
            sqlCommand.Parameters.AddWithValue("@lnameValue", HttpUtility.UrlDecode(lastName));
            sqlCommand.Parameters.AddWithValue("@emailValue", HttpUtility.UrlDecode(email));
            sqlCommand.Parameters.AddWithValue("@passwordValue", HttpUtility.UrlDecode(password));
            sqlCommand.Parameters.AddWithValue("@genre1Value", HttpUtility.UrlDecode(firstFaveGenre));
            sqlCommand.Parameters.AddWithValue("@genre2Value", HttpUtility.UrlDecode(secondFaveGenre));

            //this time, we're not using a data adapter to fill a data table.  We're just
            //opening the connection, telling our command to "executescalar" which says basically
            //execute the query and just hand me back the number the query returns (the ID, remember?).
            //don't forget to close the connection!
            sqlConnection.Open();
            //we're using a try/catch so that if the query errors out we can handle it gracefully
            //by closing the connection and moving on
            try
            {
                int accountID = Convert.ToInt32(sqlCommand.ExecuteScalar());
                //here, you could use this accountID for additional queries regarding
                //the requested account.  Really this is just an example to show you
                //a query where you get the primary key of the inserted row back from
                //the database!
            }
            catch (Exception e)
            {
            }
            sqlConnection.Close();
        }

        //EXAMPLE OF A SELECT, AND RETURNING "COMPLEX" DATA TYPES
        [WebMethod(EnableSession = true)]
        public Account[] GetAccounts()
        {
            //check out the return type.  It's an array of Account objects.  You can look at our custom Account class in this solution to see that it's 
            //just a container for public class-level variables.  It's a simple container that asp.net will have no trouble converting into json.  When we return
            //sets of information, it's a good idea to create a custom container class to represent instances (or rows) of that information, and then return an array of those objects.  
            //Keeps everything simple.

            //WE ONLY SHARE ACCOUNTS WITH LOGGED IN USERS!
            if (Session["id"] != null)
            {
                DataTable sqlDt = new DataTable("accounts");

                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelect = "select id, userid, pass, firstname, lastname, email from account where active=1 order by lastname";

                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

                //gonna use this to fill a data table
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                //filling the data table
                sqlDa.Fill(sqlDt);

                //loop through each row in the dataset, creating instances
                //of our container class Account.  Fill each acciount with
                //data from the rows, then dump them in a list.
                List<Account> accounts = new List<Account>();
                for (int i = 0; i < sqlDt.Rows.Count; i++)
                {
                    //only share user id and pass info with admins!
                    if (Convert.ToInt32(Session["admin"]) == 1)
                    {
                        accounts.Add(new Account
                        {
                            id = Convert.ToInt32(sqlDt.Rows[i]["id"]),
                            userId = sqlDt.Rows[i]["userid"].ToString(),
                            password = sqlDt.Rows[i]["pass"].ToString(),
                            firstName = sqlDt.Rows[i]["firstname"].ToString(),
                            lastName = sqlDt.Rows[i]["lastname"].ToString(),
                            email = sqlDt.Rows[i]["email"].ToString()
                        });
                    }
                    else
                    {
                        accounts.Add(new Account
                        {
                            id = Convert.ToInt32(sqlDt.Rows[i]["id"]),
                            firstName = sqlDt.Rows[i]["firstname"].ToString(),
                            lastName = sqlDt.Rows[i]["lastname"].ToString(),
                            email = sqlDt.Rows[i]["email"].ToString()
                        });
                    }
                }
                //convert the list of accounts to an array and return!
                return accounts.ToArray();
            }
            else
            {
                //if they're not logged in, return an empty array
                return new Account[0];
            }
        }

        //EXAMPLE OF AN UPDATE QUERY WITH PARAMS PASSED IN
        [WebMethod(EnableSession = true)]
        public void UpdateAccount(string firstName, string lastName, string email, string password, string firstFaveGenre, string secondFaveGenre)
        {
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //this is a simple update, with parameters to pass in values
            string sqlSelect = "update account set firstname=@firstName, lastname=@lastName, " +
                "email=@email, password=@password, firstFaveGenre=@firstFaveGenre, secondFaveGenre=@secondFaveGenre where accountID=@accountID";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@accountID", HttpUtility.UrlDecode(Session["accountID"].ToString()));
            sqlCommand.Parameters.AddWithValue("@firstName", HttpUtility.UrlDecode(firstName));
            sqlCommand.Parameters.AddWithValue("@lastName", HttpUtility.UrlDecode(lastName));
            sqlCommand.Parameters.AddWithValue("@email", HttpUtility.UrlDecode(email));
            sqlCommand.Parameters.AddWithValue("@password", HttpUtility.UrlDecode(password));
            sqlCommand.Parameters.AddWithValue("@firstFaveGenre", HttpUtility.UrlDecode(firstFaveGenre));
            sqlCommand.Parameters.AddWithValue("@secondFaveGenre", HttpUtility.UrlDecode(secondFaveGenre));

            sqlConnection.Open();
            //we're using a try/catch so that if the query errors out we can handle it gracefully
            //by closing the connection and moving on
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            sqlConnection.Close();
        }

        //EXAMPLE OF A SELECT, AND RETURNING "COMPLEX" DATA TYPES
        [WebMethod(EnableSession = true)]
        public Account[] GetAccountRequests()
        {//LOGIC: get all account requests and return them!
            if (Convert.ToInt32(Session["admin"]) == 1)
            {
                DataTable sqlDt = new DataTable("accountrequests");

                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                //requests just have active set to 0
                string sqlSelect = "select id, userid, pass, firstname, lastname, email from account where active=0 order by lastname";

                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                sqlDa.Fill(sqlDt);

                List<Account> accountRequests = new List<Account>();
                for (int i = 0; i < sqlDt.Rows.Count; i++)
                {
                    accountRequests.Add(new Account
                    {
                        id = Convert.ToInt32(sqlDt.Rows[i]["id"]),
                        firstName = sqlDt.Rows[i]["firstname"].ToString(),
                        lastName = sqlDt.Rows[i]["lastname"].ToString(),
                        email = sqlDt.Rows[i]["email"].ToString()
                    });
                }
                //convert the list of accounts to an array and return!
                return accountRequests.ToArray();
            }
            else
            {
                return new Account[0];
            }
        }

        //EXAMPLE OF A DELETE QUERY
        [WebMethod(EnableSession = true)]
        public void DeleteAccount(string id)
        {
            if (Convert.ToInt32(Session["admin"]) == 1)
            {
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                //this is a simple update, with parameters to pass in values
                string sqlSelect = "delete from account where id=@idValue";

                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(id));

                sqlConnection.Open();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                sqlConnection.Close();
            }
        }

        //EXAMPLE OF AN UPDATE QUERY
        [WebMethod(EnableSession = true)]
        public void ActivateAccount(string id)
        {
            if (Convert.ToInt32(Session["admin"]) == 1)
            {
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                //this is a simple update, with parameters to pass in values
                string sqlSelect = "update account set active=1 where id=@idValue";

                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(id));

                sqlConnection.Open();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                sqlConnection.Close();
            }
        }

        //EXAMPLE OF A DELETE QUERY
        [WebMethod(EnableSession = true)]
        public void RejectAccount(string id)
        {
            if (Convert.ToInt32(Session["admin"]) == 1)
            {
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelect = "delete from account where id=@idValue";

                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(id));

                sqlConnection.Open();
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                sqlConnection.Close();
            }
        }

        //NNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNEW CODE AFTER 2/14
        [WebMethod(EnableSession = true)]
        public Account[] CheckEmail(string email)
        {
            DataTable sqlDt = new DataTable("accounts");

            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            string sqlSelect = "select email from account where email=@tmpEmailId";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@tmpEmailId", HttpUtility.UrlDecode(email));

            //gonna use this to fill a data table
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            //filling the data table
            sqlDa.Fill(sqlDt);

            //loop through each row in the dataset, creating instances
            //of our container class Account.  Fill each acciount with
            //data from the rows, then dump them in a list.
            List<Account> emailAccounts = new List<Account>();
            for (int i = 0; i < sqlDt.Rows.Count; i++)
            {
                emailAccounts.Add(new Account
                {
                    email = sqlDt.Rows[i]["email"].ToString()
                });
            }
            //convert the list of accounts to an array and return!
            return emailAccounts.ToArray();
        }



        //New as of 2/15
        //EXAMPLE OF A SIMPLE SELECT QUERY (PARAMETERS PASSED IN FROM CLIENT)
        [WebMethod(EnableSession = true)] //NOTICE: gotta enable session on each individual method
        public LogonInfo LogOn(string email, string password)
        {
            //we return this flag to tell them if they logged in or not
            bool success = false;

            //our connection string comes from our web.config file like we talked about earlier
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
            //NOTICE: we added admin to what we pull, so that we can store it along with the id in the session
            string sqlSelect = "SELECT * FROM account WHERE email=@emailValue and password=@passValue";

            //set up our connection object to be ready to use our connection string
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            //set up our command object to use our connection, and our query
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            //tell our command to replace the @parameters with real values
            //we decode them because they came to us via the web so they were encoded
            //for transmission (funky characters escaped, mostly)
            sqlCommand.Parameters.AddWithValue("@emailValue", HttpUtility.UrlDecode(email));
            sqlCommand.Parameters.AddWithValue("@passValue", HttpUtility.UrlDecode(password));

            //a data adapter acts like a bridge between our command object and 
            //the data we are trying to get back and put in a table object
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            //here's the table we want to fill with the results from our query
            DataTable sqlDt = new DataTable();
            //here we go filling it!
            sqlDa.Fill(sqlDt);
            //check to see if any rows were returned.  If they were, it means it's 
            //a legit account
            LogonInfo toReturn = new LogonInfo();
            toReturn.successfulLogon = false;
            if (sqlDt.Rows.Count > 0)
            {
                //if we found an account, store the id and admin status in the session
                //so we can check those values later on other method calls to see if they 
                //are 1) logged in at all, and 2) and admin or not
                Session["accountID"] = sqlDt.Rows[0]["accountID"];
                //Session["AdminAbility"] = sqlDt.Rows[0]["AdminAbility"];
                //Session["activeAccount"] = sqlDt.Rows[0]["activeAccount"];
                //Session["adminAbility"] = sqlDt.Rows[0]["adminAbility"];
                success = true;
                toReturn.successfulLogon = true;
                if (sqlDt.Rows[0]["AdminAbility"].ToString() == "True")
                {
                    toReturn.admin = true;
                }
                else
                {
                    toReturn.admin = false;
                }

                if (sqlDt.Rows[0]["activeAccount"].ToString() == "True")
                {
                    toReturn.active = true;
                }
                else
                {
                    toReturn.active = false;
                }
                toReturn.accountID = sqlDt.Rows[0]["accountID"].ToString();
            }
            //return the result!
            return toReturn;

        } // end of LogOn

        // from Thomas
        [WebMethod(EnableSession = true)]
        public Quote SearchQuotes()
        {
            //here we are issuing the SQL statement that pulls Influental catagory 
            DataTable sqlDtQuotes = new DataTable("quotes2");
            string sqlConnectStringAccount = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            string sqlSelectAccount = "SELECT quoteID, quoteText, qFirstName, qLastName, rating, genre1, genre2, genre3 FROM quotes2";

            MySqlConnection sqlConnectionAccount = new MySqlConnection(sqlConnectStringAccount);
            MySqlCommand sqlCommandAccount = new MySqlCommand(sqlSelectAccount, sqlConnectionAccount);

            // sqlCommandAccount.Parameters.AddWithValue("@genreVAL", HttpUtility.UrlDecode(gid));

            MySqlDataAdapter sqlDaAccount = new MySqlDataAdapter(sqlCommandAccount);
            sqlDaAccount.Fill(sqlDtQuotes);

            List<Quote> influentialQuotes = new List<Quote>();
            for (int i = 0; i < sqlDtQuotes.Rows.Count; i++)
            {


                influentialQuotes.Add(new Quote
                {
                    QuoteId = Convert.ToInt32(sqlDtQuotes.Rows[i]["quoteID"]),
                    QuoteText = sqlDtQuotes.Rows[i]["quoteText"].ToString(),
                    QFirstName = sqlDtQuotes.Rows[i]["qFirstName"].ToString(),
                    QLastName = sqlDtQuotes.Rows[i]["qLastName"].ToString(),
                    Rating = Convert.ToInt32(sqlDtQuotes.Rows[i]["rating"]),
                    Genre1 = sqlDtQuotes.Rows[i]["genre1"].ToString(),
                    Genre2 = sqlDtQuotes.Rows[i]["genre2"].ToString(),
                    Genre3 = sqlDtQuotes.Rows[i]["genre3"].ToString()
                });

            }


            influentialQuotes.ToArray();

            Random randomNumber = new Random();
            int quoteNumber = randomNumber.Next(0, influentialQuotes.Count);

            return influentialQuotes[quoteNumber];


        } // end of Thomas' work

        //EXAMPLE OF A SELECT, AND RETURNING "COMPLEX" DATA TYPES
        [WebMethod(EnableSession = true)]
        public Quote GetQuotes()
        {
            //date and time variables
            DateTime yesterday = DateTime.Now;
            int yesterDayNumber = yesterday.DayOfYear;
            DateTime now = DateTime.Now;
            int nowDayNumber = now.DayOfYear;

            //verifies the existence of the accountID in the account_quotes table
            if (ExistingAccount_Quote(Convert.ToInt32(Session["accountID"])) == true)
            {
                //here we are checking the account_quotes table  and assigning accountID and quotes ID as wellas the account quote last updated field
                DataTable sqlDtAccountQuote = new DataTable("account_quotes");
                string sqlConnectStringAccountQuote = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelectAccountQuote = "SELECT accountID, quoteID, quoteLastUpdated from account_quotes where accountID=" + Session["accountID"];
                MySqlConnection sqlConnectionAccountQuote = new MySqlConnection(sqlConnectStringAccountQuote);
                MySqlCommand sqlCommandAccountQuote = new MySqlCommand(sqlSelectAccountQuote, sqlConnectionAccountQuote);
                MySqlDataAdapter sqlDaAccountQuote = new MySqlDataAdapter(sqlCommandAccountQuote);
                sqlDaAccountQuote.Fill(sqlDtAccountQuote);

                int account_quotes_accountID = Convert.ToInt32(sqlDtAccountQuote.Rows[0][0]);
                int account_quotes_quoteID = Convert.ToInt32(sqlDtAccountQuote.Rows[0][1]);
                int account_quotes_quoteLastUpdated = Convert.ToInt32(sqlDtAccountQuote.Rows[0][2]);

                //making a decisiion based on when the quote was last updated
                if (yesterDayNumber != nowDayNumber)
                {
                    //up here, we grab the person's quote preference from the account table
                    DataTable sqlDtAccount = new DataTable("account");
                    string sqlConnectStringAccount = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                    string sqlSelectAccount = "SELECT firstFaveGenre, secondFaveGenre from account where accountID=" + Session["accountID"];
                    MySqlConnection sqlConnectionAccount = new MySqlConnection(sqlConnectStringAccount);
                    MySqlCommand sqlCommandAccount = new MySqlCommand(sqlSelectAccount, sqlConnectionAccount);
                    MySqlDataAdapter sqlDaAccount = new MySqlDataAdapter(sqlCommandAccount);
                    sqlDaAccount.Fill(sqlDtAccount);

                    string firstFaveGenre = Convert.ToString(sqlDtAccount.Rows[0][0]);
                    string secondFaveGenre = Convert.ToString(sqlDtAccount.Rows[0][1]);
                    DateTime newQuoteTime = DateTime.Today.AddDays(-1);

                    //everything below here is for getting the random quote from the quotesTable 
                    DataTable sqlDt = new DataTable("quotes");
                    string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                    string sqlSelect = "select quoteID, quoteText, author, rating, genre1, genre2, genre3 from quotes where genre1='" + firstFaveGenre + "' or genre2='" + firstFaveGenre + "' or genre3='" + firstFaveGenre + "' or genre1='" + secondFaveGenre + "' or genre2='" + secondFaveGenre + "' or genre3='" + secondFaveGenre + "'";
                    MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                    MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);
                    MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                    sqlDa.Fill(sqlDt);

                    //adds quotes to list, convert it to array, creates random number and preps to return
                    List<Quote> quotes = new List<Quote>();
                    for (int i = 0; i < sqlDt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(Session["admin"]) == 1)
                        {
                            quotes.Add(new Quote
                            {
                                QuoteId = Convert.ToInt32(sqlDt.Rows[i]["quoteID"]),
                                QuoteText = sqlDt.Rows[i]["quoteText"].ToString(),
                                Author = sqlDt.Rows[i]["author"].ToString(),
                                Rating = Convert.ToInt32(sqlDt.Rows[i]["rating"]),
                                Genre1 = sqlDt.Rows[i]["genre1"].ToString(),
                                Genre2 = sqlDt.Rows[i]["genre2"].ToString(),
                                Genre3 = sqlDt.Rows[i]["genre3"].ToString()
                            });
                        }
                        else
                        {
                            quotes.Add(new Quote
                            {
                                QuoteId = Convert.ToInt32(sqlDt.Rows[i]["quoteID"]),
                                QuoteText = sqlDt.Rows[i]["quoteText"].ToString(),
                                Author = sqlDt.Rows[i]["author"].ToString(),
                                Rating = Convert.ToInt32(sqlDt.Rows[i]["rating"]),
                                Genre1 = sqlDt.Rows[i]["genre1"].ToString(),
                                Genre2 = sqlDt.Rows[i]["genre2"].ToString(),
                                Genre3 = sqlDt.Rows[i]["genre3"].ToString()
                            });
                        }
                    }

                    quotes.ToArray(); //turns list into array
                    Random randomNumber = new Random();
                    int quoteNumber = randomNumber.Next(0, quotes.Count);

                    //update the last time the quote was updated and the quoteID and accountID pairing.
                    string sqlConnectStringUpdate = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                    string sqlSelectUpdate = "UPDATE account_quotes SET quoteID=" + quotes[quoteNumber].QuoteId + ", quoteLastUpdated=" + nowDayNumber + " WHERE accountId=" + Session["accountID"];

                    MySqlConnection sqlConnectionUpdate = new MySqlConnection(sqlConnectStringUpdate);
                    MySqlCommand sqlCommandUpdate = new MySqlCommand(sqlSelectUpdate, sqlConnectionUpdate);

                    sqlConnectionUpdate.Open();
                    sqlCommandUpdate.ExecuteNonQuery();
                    sqlConnectionUpdate.Close();

                    //returns a single quote at a random location in the array
                    return quotes[quoteNumber];
                }
                else
                {
                    //grabs the quote information from the quote table based on the quoteID
                    DataTable sqlDtAccount = new DataTable("quotes");
                    string sqlConnectStringAccount = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                    string sqlSelectAccount = "SELECT quoteID, quoteText, author, rating, genre1, genre2, genre3 from quotes where quoteID=" + account_quotes_quoteID;
                    MySqlConnection sqlConnectionAccount = new MySqlConnection(sqlConnectStringAccount);
                    MySqlCommand sqlCommandAccount = new MySqlCommand(sqlSelectAccount, sqlConnectionAccount);
                    MySqlDataAdapter sqlDaAccount = new MySqlDataAdapter(sqlCommandAccount);
                    sqlDaAccount.Fill(sqlDtAccount);

                    Quote assignedQuote = new Quote();

                    assignedQuote.QuoteId = Convert.ToInt32(sqlDtAccount.Rows[0][0]);
                    assignedQuote.QuoteText = Convert.ToString(sqlDtAccount.Rows[0][1]);
                    assignedQuote.Author = Convert.ToString(sqlDtAccount.Rows[0][2]);
                    assignedQuote.Rating = Convert.ToInt32(sqlDtAccount.Rows[0][3]);
                    assignedQuote.Genre1 = Convert.ToString(sqlDtAccount.Rows[0][4]);
                    assignedQuote.Genre2 = Convert.ToString(sqlDtAccount.Rows[0][5]);
                    assignedQuote.Genre3 = Convert.ToString(sqlDtAccount.Rows[0][6]);

                    return assignedQuote;
                }
            }
            else
            {
                //grabs the user's quote preference
                DataTable sqlDtAccount = new DataTable("account");
                string sqlConnectStringAccount = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelectAccount = "SELECT firstFaveGenre, secondFaveGenre from account where accountID=" + Session["accountID"];
                MySqlConnection sqlConnectionAccount = new MySqlConnection(sqlConnectStringAccount);
                MySqlCommand sqlCommandAccount = new MySqlCommand(sqlSelectAccount, sqlConnectionAccount);
                MySqlDataAdapter sqlDaAccount = new MySqlDataAdapter(sqlCommandAccount);
                sqlDaAccount.Fill(sqlDtAccount);

                string firstFaveGenre = Convert.ToString(sqlDtAccount.Rows[0][0]);
                string secondFaveGenre = Convert.ToString(sqlDtAccount.Rows[0][1]);

                //everything below here is for getting the random quote from the quotesTable 
                DataTable sqlDt = new DataTable("quotes");
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelect = "select quoteID, quoteText, author, rating, genre1, genre2, genre3 from quotes where genre1='" + firstFaveGenre + "' or genre2='" + firstFaveGenre + "' or genre3='" + firstFaveGenre + "' or genre1='" + secondFaveGenre + "' or genre2='" + secondFaveGenre + "' or genre3='" + secondFaveGenre + "'";
                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                sqlDa.Fill(sqlDt);

                //adds quotes to list, convert it to array, creates random number and preps to return
                List<Quote> quotes = new List<Quote>();
                for (int i = 0; i < sqlDt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(Session["admin"]) == 1)
                    {
                        quotes.Add(new Quote
                        {
                            QuoteId = Convert.ToInt32(sqlDt.Rows[i]["quoteID"]),
                            QuoteText = sqlDt.Rows[i]["quoteText"].ToString(),
                            Author = sqlDt.Rows[i]["author"].ToString(),
                            Rating = Convert.ToInt32(sqlDt.Rows[i]["rating"]),
                            Genre1 = sqlDt.Rows[i]["genre1"].ToString(),
                            Genre2 = sqlDt.Rows[i]["genre2"].ToString(),
                            Genre3 = sqlDt.Rows[i]["genre3"].ToString()
                        });
                    }
                    else
                    {
                        quotes.Add(new Quote
                        {
                            QuoteId = Convert.ToInt32(sqlDt.Rows[i]["quoteID"]),
                            QuoteText = sqlDt.Rows[i]["quoteText"].ToString(),
                            Author = sqlDt.Rows[i]["author"].ToString(),
                            Rating = Convert.ToInt32(sqlDt.Rows[i]["rating"]),
                            Genre1 = sqlDt.Rows[i]["genre1"].ToString(),
                            Genre2 = sqlDt.Rows[i]["genre2"].ToString(),
                            Genre3 = sqlDt.Rows[i]["genre3"].ToString()
                        });
                    }
                }

                quotes.ToArray(); //turns list into array
                Random randomNumber = new Random();
                int quoteNumber = randomNumber.Next(0, quotes.Count);
                string aID = Convert.ToString(Session["accountID"]);

                //update the last time the quote was updated and the quoteID and accountID pairing.
                string sqlConnectStringUpdate = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                string sqlSelectUpdate = "INSERT INTO account_quotes(accountID, quoteID, quoteLastUpdated) VALUES(" + aID + "," + quotes[quoteNumber].QuoteId + "," + nowDayNumber + ")";



                MySqlConnection sqlConnectionUpdate = new MySqlConnection(sqlConnectStringUpdate);
                MySqlCommand sqlCommandUpdate = new MySqlCommand(sqlSelectUpdate, sqlConnectionUpdate);

                sqlConnectionUpdate.Open();
                sqlCommandUpdate.ExecuteNonQuery();
                sqlConnectionUpdate.Close();
                return quotes[quoteNumber];
            }


        }
        //CHECKS FOR THE EXISTANCE OF AN ACCOUNT ID WITHIN THE ACCOUNT_QUOTES
        [WebMethod(EnableSession = true)]
        public bool ExistingAccount_Quote(int sessionID)
        {
            //method variable
            bool exist = false;

            //establishes a connection and includes sql statement to pull from account_quotes
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            string sqlSelect = "SELECT * FROM account_quotes WHERE accountID=" + sessionID;
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            DataTable sqlDt = new DataTable();
            sqlDa.Fill(sqlDt);

            //checks to see if the account is here
            LogonInfo toReturn = new LogonInfo();
            toReturn.successfulLogon = false;
            if (sqlDt.Rows.Count > 0)
            {
                exist = true;
            }
            else
            {
                exist = false;
            }

            return exist;
        }

    }
}

