//Alphabetical order
//Alphabetical order
//Alphabetical order




//we're using a stacked card approach for our main viewing area
//this array holds the IDS of our cards and the method
var contentPanels = ['LogInPanel', 'registerUserPanel', 'homePagePanel', 'CreatePosts'];
		//this function toggles which panel is showing, and also clears data from all panels
function showPanel(panelId) {
    //Consider function to clear other data panels if not done on other functions

    for (var i = 0; i < contentPanels.length; i++) {
        if (panelId == contentPanels[i]) {
            $("#" + contentPanels[i]).css("display", "block");
        }
        else {
            $("#" + contentPanels[i]).css("display", "none");
        }
    }

    //to call put ShowPanel(Panel Id you want to show)
}

//takes post information from the html page and uploads it to the database
function CreatePost() {
    // take all input
    postTitle = document.getElementById('postTitleID').value
    postText = document.getElementById('postDescriptionID').value
    actionableItem = document.getElementById('postSolutionID').value
    postUsername = "hardcoded value: chicken nuggets";

    // test to see if post text or author's name are missing
    if (postTitle == "" || postText == "" || actionableItem == "") {
        alert("Please be sure to enter both the quote and its author. If there is no author, simply write \"unknown\".");
    }
    else {
        var webMethod = "AccountServices.asmx/CreatePost";

        var parameters = "{\"postTitle\":\"" + encodeURI(postTitle) + "\",\"postText\":\"" + encodeURI(postText) + "\",\"actionableItem\":\"" + encodeURI(actionableItem) + "\",\"postUsername\":\"" + encodeURI(postUsername) + "\"}";
        $.ajax({
            type: "POST",
            url: webMethod,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                alert("Thank you for your post");
                document.getElementById('postTitleID').value = "we are in the success section"
                document.getElementById('postDescriiptionID').value = "we are in the success section"
                document.getElementById('postSolutionID').value = "we are in the success section"
            },
            error: function (e) {
                alert("boo...");
            }
        });
    }

}

function generatePosts() {

    var webMethod = "AccountServices.asmx/GeneratePost";
    $.ajax({
        type: "POST",
        url: webMethod,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            for (var i = 0; i < msg.d.length; i++){
                var postID = msg.d[i].PostID;
                var postTitle = msg.d[i].PostTitle;
                var postText = msg.d[i].PostText;
                var postActionableItem = msg.d[i].ActionableItem;
                var postUsername = msg.d[i].PostUsername;
                var postDateTime = msg.d[i].PostDateTime;
                var postSensored = msg.d[i].IsSensored;

                // inserting into the cards
                var div = document.createElement('div');
                div.setAttribute("class", "card");
                //= 'card';
                div.style = "width: 70%";

                var actualCard = document.createElement('img')
                actualCard.setAttribute("class", 'card-img-top');

                var divInsideCard = document.createElement('div');
                divInsideCard.setAttribute("class", "card-body");

                var header = document.createElement('h5');
                header.setAttribute("class", "card-title");
                header.innerHTML = postTitle;

                var paragraph = document.createElement('p');
                paragraph.setAttribute("class", "card-text")
                //paragraph.header.setAttribute("class",  "card-text")
                paragraph.innerHTML = postText;

                var paragraphActionable = document.createElement('p');
                paragraph.setAttribute("class", "card-text")
                //paragraph.header.setAttribute("class",  "card-text")
                paragraphActionable.innerHTML = postActionableItem;

                var commentButton = document.createElement('a');
                commentButton.innerHTML = "Comment";
                commentButton.setAttribute("class", "btn btn-primary");
                commentButton.setAttribute("href", "#");

                var likeButton = document.createElement('a');
                likeButton.innerHTML = "Like";
                likeButton.setAttribute("class", "btn  btn-primary");
                likeButton.setAttribute("href", "#");

                var dislikeButton = document.createElement('a');
                dislikeButton.innerHTML = "Dislike";
                dislikeButton.setAttribute("class", "btn  btn-primary");
                dislikeButton.setAttribute("href", "#");

                //appends everything
                divInsideCard.appendChild(header);
                divInsideCard.appendChild(paragraph);
                divInsideCard.appendChild(paragraphActionable);
                divInsideCard.appendChild(commentButton);
                divInsideCard.appendChild(likeButton);
                divInsideCard.appendChild(dislikeButton);

                div.appendChild(divInsideCard);

                document.body.appendChild(div);
            }
            
        },
        error: function (e) {
            alert("boo...");
        }
    })


    //var numberOfDivs = 5;

    //for (var i = 0; i < numberOfDivs; i++) {
    //    var div = document.createElement('div');
    //    div.setAttribute("class", "card");
    //    //= 'card';
    //    div.style = "width: 70%";

    //    var actualCard = document.createElement('img')
    //    actualCard.setAttribute("class", 'card-img-top');

    //    var divInsideCard = document.createElement('div');
    //    divInsideCard.setAttribute("class", "card-body");

    //    var header = document.createElement('h5');
    //    header.setAttribute("class", "card-title");
    //    header.innerHTML = "Card Title"

    //    var paragraph = document.createElement('p');
    //    paragraph.setAttribute("class", "card-text")
    //    //paragraph.header.setAttribute("class",  "card-text")
    //    paragraph.innerHTML = "Some quick example text to build on the card title and make up the bulk of the card's content";

    //    var commentButton = document.createElement('a');
    //    commentButton.innerHTML = "Comment";
    //    commentButton.setAttribute("class", "btn btn-primary");
    //    commentButton.setAttribute("href", "#");

    //    var likeButton = document.createElement('a');
    //    likeButton.innerHTML = "Like";
    //    likeButton.setAttribute("class", "btn  btn-primary");
    //    likeButton.setAttribute("href", "#");

    //    var dislikeButton = document.createElement('a');
    //    dislikeButton.innerHTML = "Dislike";
    //    dislikeButton.setAttribute("class", "btn  btn-primary");
    //    dislikeButton.setAttribute("href", "#");

    //    //appends everything
    //    divInsideCard.appendChild(header);
    //    divInsideCard.appendChild(paragraph);
    //    divInsideCard.appendChild(commentButton);
    //    divInsideCard.appendChild(likeButton);
    //    divInsideCard.appendChild(dislikeButton);

    //    div.appendChild(divInsideCard);

    //    document.body.appendChild(div);
    //}
}


function generateCommentsBox() {
    //creates a div element and gives it a class name
    var div = document.createElement('div');
    div.className = 'form-group';

    //creates a label element, gives it a class name, rites it.
    var label = document.createElement('label');
    label.className = 'col-sm-2 control-label';
    label.innerHTML = 'Comments';
    label.for = 'inputText';

    //creates div and gives it class 
    var div1 = document.createElement('div');
    div1.className = 'col-sm-10';

    //creates a textArea and call it commentText. also sets the id, rows and other things too
    var commentText = document.createElement('textarea');
    commentText.className = 'form-control';
    commentText.id = 'inputText';
    commentText.rows = '3';
    commentText.placeholder = 'Write your comments';

    //appends label to div
    div.appendChild(label);

    //appends commentText to div1
    div1.appendChild(commentText);

    //appends div1 to div
    div.appendChild(div1);

    document.body.appendChild(div);
}


































































































































































































//Previous project code
var SessionID = {
    admin: false,
    id: 0,
    active: false,
};

function startPage() {
    document.getElementById("loadAdminPageId").style.display = 'none';
}

//Log the user
	//if correct take to home page
	//else clear log in information
function backHome() {
	//Takes the user back to home page
	window.location.replace("HomePage.html");
}

function submitUserEditInfo() {
    //gets information from textBoxes
    fName = document.getElementById('editFirstNameId').value;
    lName = document.getElementById('editLastNameId').value;
    testemail = document.getElementById('editEmailId').value;
    regpassword = document.getElementById('editPasswordId').value;
    genre1 = document.getElementById('editGenre1Id').value;
    genre2 = document.getElementById('editGenre2Id').value;

    //store to lower
    fName.toLowerCase();
    lName.toLowerCase();
    testemail.toLowerCase();

    //test if any of the fields are empty
    if (fName == "" || lName == "" || regpassword == "" || testemail == "") {
        alert("Please be sure to fill out all criteria");
    }
    else {
        //Test if password has symbol, lettter, and number
        //symbol
        if (regpassword.includes("!") || regpassword.includes("@") || regpassword.includes("#") || regpassword.includes("$") || regpassword.includes("%") || regpassword.includes("^") || regpassword.includes("&") || regpassword.includes("*")) {
            //number
            if (regpassword.includes("1") || regpassword.includes("2") || regpassword.includes("3") || regpassword.includes("4") || regpassword.includes("5") || regpassword.includes("6") || regpassword.includes("7") || regpassword.includes("8") || regpassword.includes("9") || regpassword.includes("0")) {
                //letter
                if (regpassword.includes("a") || regpassword.includes("b") || regpassword.includes("c") || regpassword.includes("d") || regpassword.includes("e") || regpassword.includes("f") || regpassword.includes("g") || regpassword.includes("h") || regpassword.includes("i") || regpassword.includes("j") || regpassword.includes("k") || regpassword.includes("l") || regpassword.includes("m") || regpassword.includes("n") || regpassword.includes("o") || regpassword.includes("p") || regpassword.includes("q") || regpassword.includes("r") || regpassword.includes("s") || regpassword.includes("t") || regpassword.includes("u") || regpassword.includes("v") || regpassword.includes("w") || regpassword.includes("x") || regpassword.includes("y") || regpassword.includes("z")) {
                    //test if email is unique
                    if (testemail.includes("@gmail.com") || testemail.includes("@asu.edu") || testemail.includes("@yahoo.com") || testemail.includes("@hotmail.com")) {
                        window.alert('all fields work');
                        //edit users information in sql

                        var webMethod = "AccountServices.asmx/UpdateAccount";
                        var parameters = "{\"firstName\":\"" + encodeURI(fName) + "\",\"lastName\":\"" + encodeURI(lName) + "\",\"email\":\"" + encodeURI(testemail) + "\",\"password\":\"" + encodeURI(regpassword) + "\",\"firstFaveGenre\":\"" + encodeURI(genre1) + "\",\"secondFaveGenre\":\"" + encodeURI(genre2) + "\"}";

                        $.ajax({
                            type: "POST",
                            url: webMethod,
                            data: parameters,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                //takes the user back home
                                window.location.replace("HomePage.html");
                            },
                            error: function (e) {
                                alert("boo...");
                            }
                        });
                    }
                    else {
                        alert("We only accept emails from: Yahoo, gmain, asu, and hotmail");
                        document.getElementById('regEmailId').value = "";
                    }
                }
                else {
                    alert("Passwords must include a lowercase letter (abcdefghijklmnopqrstuvwxyz)")
                    document.getElementById('regPasswordId').value = "";
                }
            }
            else {
                alert("Passwords must include a number (0123456789)")
                document.getElementById('regPasswordId').value = "";
            }
        }
        else {
            alert("Passwords must include a symbol (!@#$%^&*)")
            document.getElementById('regPasswordId').value = ""
        }
    }

}

function forgotPassword() {
	//send them an email
	//Take them back to log in page

}

function loadAddQuotePage() {
	window.location.replace("UploadQuote.html")
}

function loadAdminPage() {
	window.location.replace("AdminPage.html")
}

function loadforgotPassword() {
	window.location.replace("ForgotPassword.html")
}

function loadregisterUserPage() {
	//Loads the register user page
	window.location.replace("RegisterUser.html");
}

function logoutUser() {
	//Takes the user back to the log In page
    
	//reset the client holding variable if used
	
}

function CreateAccount(fName, lName, email, password, genre1, genre2) {
    var webMethod = "AccountServices.asmx/RequestAccount";
    var parameters = "{\"firstName\":\"" + encodeURI(fName) + "\",\"lastName\":\"" + encodeURI(lName) + "\",\"email\":\"" + encodeURI(email) + "\",\"password\":\"" + encodeURI(password) + "\",\"firstFaveGenre\":\"" + encodeURI(genre1) + "\",\"secondFaveGenre\":\"" + encodeURI(genre2)+"\"}";

    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            alert("Thank you for registering!");
            window.location.replace("logInPage.html");
        },
        error: function (e) {
            alert("Something went wrong please try again later");
        }
    });
}


function editUserInformation() {
    //edit users information in sql

    //takes the user back home
    window.location.replace("EditUserInformation.html");
}

function submitAddQuote() {
	//insert quote into SQL statement

	//return to home page if it works
	window.location.replace("HomePage.html")
}
function SubmitSearch(){
	//user the id from the search input box 

	// print the results to the text area
}

//New Code post 2/14 class
function registerUser() {
    //take all input
    fname = document.getElementById('regFirstNameId').value
    lname = document.getElementById('regLastNameId').value
    regpassword = document.getElementById('regPasswordId').value
    testemail = document.getElementById('regEmailId').value
    genre1 = document.getElementById('RegGenre2Id').value
    genre2 = document.getElementById('RegGenre2Id').value

    //store to lower  
    fname.toLowerCase();
    lname.toLowerCase();
    testemail.toLowerCase();
    //test if any are  empty
    if (fname == "" || lname == "" || regpassword == "" || testemail == "") {
        alert("Please be sure to fill out all criteria");
    }
    else {
        //Test if password has symbol, lettter, and number
        //symbol
        if (regpassword.includes("!") || regpassword.includes("@") || regpassword.includes("#") || regpassword.includes("$") || regpassword.includes("%") || regpassword.includes("^") || regpassword.includes("&") || regpassword.includes("*")) {
            //number
            if (regpassword.includes("1") || regpassword.includes("2") || regpassword.includes("3") || regpassword.includes("4") || regpassword.includes("5") || regpassword.includes("6") || regpassword.includes("7") || password.includes("8") || regpassword.includes("9") || regpassword.includes("0")) {
                //letter
                if (regpassword.includes("a") || regpassword.includes("b") || regpassword.includes("c") || regpassword.includes("d") || regpassword.includes("e") || regpassword.includes("f") || regpassword.includes("g") || regpassword.includes("h") || regpassword.includes("i") || regpassword.includes("j") || regpassword.includes("k") || regpassword.includes("l") || regpassword.includes("m") || regpassword.includes("n") || regpassword.includes("o") || regpassword.includes("p") || regpassword.includes("q") || regpassword.includes("r") || regpassword.includes("s") || regpassword.includes("t") || regpassword.includes("u") || regpassword.includes("v") || regpassword.includes("w") || regpassword.includes("x") || regpassword.includes("y") || regpassword.includes("z")) {
                    //test if email is unique
                    if (testemail.includes("@gmail.com") || testemail.includes("@asu.edu") || testemail.includes("@yahoo.com") || testemail.includes("@hotmail.com")) {
                        ReqTestPass(testemail)
                        //test email contains . and @
                    }
                    else {
                        alert("We only accept emails from: Yahoo, gmain, asu, and hotmail");
                        document.getElementById('regEmailId').value = "";
                    }
                }              
                else {
                    alert("Passwords must include a lowercase letter (abcdefghijklmnopqrstuvwxyz)")
                    document.getElementById('regPasswordId').value = "";
                }
            }
            else {
                alert("Passwords must include a number (0123456789)")
                document.getElementById('regPasswordId').value = "";   
            }              
        }
        else {
            alert("Passwords must include a symbol (!@#$%^&*)")
            document.getElementById('regPasswordId').value = ""
        }   
    }
}

function ReqTestPass(tmpEmail) {
    //var webMethod = "AccountServices.asmx/GetAccounts";
    var webMethod = "AccountServices.asmx/CheckEmail";   
    var parameters = "{\"email\":\"" + encodeURI(tmpEmail) + "\"}";
    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d.length > 0) {
                emailArray = msg.d;
                //console.log(emailArray); //for debugging
                tmp = true;
                for (var i = 0; i < emailArray.length; i++) {
                    //console.log(emailArray[i].email); //for debugging
                    //console.log(tmpEmail); //for debugging
                    str1 = emailArray[i].email;
                    console.log(str1); //for debugging
                    console.log(tmpEmail); // for debugging
                    str1.toLowerCase();
                    if (str1 == tmpEmail) {
                        document.getElementById('regEmailId').value = ""
                        return alert("This Email is already in use please use another")
                        
                    }
                }
                //submit form with info
                CreateAccount($('#regFirstNameId').val(), $('#regLastNameId').val(), $('#regEmailId').val(), $('#regPasswordId').val(), $('#RegGenre1Id').val(), $('#RegGenre2Id').val()); //return false;
                //clear old information
                document.getElementById('regFirstNameId').value = "";
                document.getElementById('regLastNameId').value = "";
                document.getElementById('regPasswordId').value = "";
                document.getElementById('regEmailId').value = "";
                document.getElementById('RegGenre2Id').value = "";
                document.getElementById('RegGenre2Id').value = "";
            }
            else {
                //submit form with info
                CreateAccount($('#regFirstNameId').val(), $('#regLastNameId').val(), $('#regEmailId').val(), $('#regPasswordId').val(), $('#RegGenre1Id').val(), $('#RegGenre2Id').val()); //return false;
                //clear old information
                document.getElementById('regFirstNameId').value = "";
                document.getElementById('regLastNameId').value = "";
                document.getElementById('regPasswordId').value = "";
                document.getElementById('regEmailId').value = "";
                document.getElementById('RegGenre2Id').value = "";
                document.getElementById('RegGenre2Id').value = "";
            }
        },
        error: function (e) {
            alert("ERRRRRRROR");
        }
    });
    //console.log("THIS IS WAT I AM LOGGING");
    //console.log(tmp)
    //return tmp;
}

function logUserIn() {
    //Test if the users information is correct
    userEmail = document.getElementById("inputEmailLogInId").value
    pass = document.getElementById("inputPasswordLogInId").value
    LogOn(userEmail, pass)
    //Takes the user to the home page if their log in information is correct
    //window.location.replace("HomePage.html");

}
//New code post 2/15
//HERE'S AN EXAMPLE OF AN AJAX CALL WITH JQUERY!
function LogOn(userEmail, pass) {
    var webMethod = "AccountServices.asmx/LogOn";
    var parameters = "{\"email\":\"" + encodeURI(userEmail) + "\",\"password\":\"" + encodeURI(pass) + "\"}";

    //jQuery ajax method
    $.ajax({

        type: "POST",
        //the url is set to the string we created above
        url: webMethod,
        //same with the data
        data: parameters,
        //these next two key/value pairs say we intend to talk in JSON format
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (LogonInfo) {
            console.log(LogonInfo);
            //the server response is in the msg object passed in to the function here
            //since our logon web method simply returns a true/false, that value is mapped
            //to a generic property of the server response called d (I assume short for data
            //but honestly I don't know...)
            if (LogonInfo.d.successfulLogon == true) {
                //server replied true, so show the accounts panel

                //sets the session ID to be used later
                SessionID.admin = LogonInfo.d.admin;
                SessionID.active = LogonInfo.d.active;
                SessionID.id = LogonInfo.d.accountID;

                //console.log("This is where I re set the session")
                //console.log(SessionID); //For debugging
                //store.set(SessionID);
                window.location.href = "HomePage.html";
            }
            else {
                //server replied false, so let the user know
                //the logon failed
                alert("logon failed");
            }
        },
        error: function (e) {
            alert("boo...");
        }
    });
}

function logoutUser() {
        var webMethod = "AccountServices.asmx/LogOff";
        $.ajax({
            type: "POST",
            url: webMethod,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d) {
                    //we logged off, so go back to logon page,
                    //Clear the session
                    window.location.href = "logInPage.html"
                }
                else {
                }
            },
            error: function (e) {
                alert("boo...");
            }
        });
}

function uploadQuote() {
    // take all input
    quote = document.getElementById('userQuoteId').value
    authorFirstName = document.getElementById('afirstName').value
    authorLastName = document.getElementById('alastName').value
    genre1 = document.getElementById('genre1').value
    genre2 = document.getElementById('genre2').value
    genre3 = document.getElementById('genre3').value
    rating = document.getElementById('initialRatingId').value


    // test to see if quote text or author's name are missing
    if (quote == "" || authorLastName == "" || authorFirstName == "") {
        alert("Please be sure to enter both the quote and its author. If there is no author, simply write \"unknown\".");
    }
    else {

        var webMethod = "AccountServices.asmx/UploadTheirQuote";

        var parameters = "{\"quote\":\"" + encodeURI(quote) + "\",\"firstName\":\"" + encodeURI(authorFirstName) + "\",\"LastName\":\"" + encodeURI(authorLastName) + "\",\"rating\":\"" + encodeURI(rating) + "\",\"genre1\":\"" + encodeURI(genre1) + "\",\"genre2\":\"" + encodeURI(genre2) + "\",\"genre3\":\"" + encodeURI(genre3) + "\"}";
        //jQuery ajax method
        $.ajax({
            type: "POST",
            //the url is set to the string we created above
            url: webMethod,
            //same with the data
            data: parameters,
            //these next two key/value pairs say we intend to talk in JSON format
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            //jQuery sends the data and asynchronously waits for a response.  when it
            //gets a response, it calls the function mapped to the success key here
            success: function (msg) {
                //the server response is in the msg object passed in to the function here
                //since our logon web method simply returns a true/false, that value is mapped
                //to a generic property of the server response called d (I assume short for data
                //but honestly I don't know...)
                if (msg.d) {
                    //server replied true, so show the accounts panel
                    // window.location.replace("HomePage.html")
                    alert("Thank you for your quote");
                    document.getElementById('userQuoteId').value =""
                    document.getElementById('afirstName').value = ""
                    document.getElementById('alastName').value = ""
                    document.getElementById('genre1').value = "None"
                    document.getElementById('genre2').value = "None"
                    document.getElementById('genre3').value = "None"
                    document.getElementById('initialRatingId').value =""
                }
                else {
                    //server replied false, so let the user know
                    //the logon failed
                    alert("logon failed");
                }
            },
            error: function (e) {
                //if something goes wrong in the mechanics of delivering the
                //message to the server or the server processing that message,
                //then this function mapped to the error key is executed rather
                //than the one mapped to the success key.  This is just a garbage
                //alert becaue I'm lazy
                alert("boo...");
            }
        });
    }



}
