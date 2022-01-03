var timeLeft = 29;
var isPaused = false;
var endReached = false;
var timerId;

$(document).ready(function () {


    //הוצאת מספר הקונטייר שהמשתמש לחץ עליו
    var ContainerID = document.getElementById('NavHiddenField').value;

    //החבאת כל הקונטיירנים בטעינת הדף
    $(".container").hide();

    //לולאה שרצה על כל התמונות בתפריט הניווט ומשנה את העיצוב 
    $(".navBtnImage").each(function (index, value) {

        $(this).css({ "height": "25px", "width": "25px" });

        if (ContainerID == $(this).attr("imageNum")) {
            $(this).css({ "height": "35px", "width": "35px" });
        }
    });

    //לולאה שרצה על כל התפריט הניווט ומשנה אותם למצה פעיל או לא 
    if (UserLoggedInHiddenField.value == "true") {

        $(".navBtn").each(function (index, value) {

            $(this).css({ "pointer-events": "auto", "opacity": "1" });

        });

        $("#SignUpBtn").hide();
        $("#SignInLink").hide();
        $("#profileBottomDiv").css("display", "block");
        $("#profilePicBtn").css("display", "block");
        $("#addPicHyperLink").css("display", "block");
        $("#longlogoImg").css("display", "none");
        $("#gameTopLogo").css("display", "block");

    }
    else {

        $(".navBtn").each(function (index, value) {

            $(this).css({ "pointer-events": "none", "opacity": "0.3" });

        });

    }


    if (ContainerID == "0") {
        $("#profileContainer").show();        //הצגת הקונטייר של עמוד כניסה
        $("#proflieNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט
        $("#profileLinkButton").css({ "pointer-events": "auto", "opacity": "1" }); //החזרת פרופיל בתפריט ניווט ללינק פעיל
        $("#emailSignUpDiv").show();  //הצגת הקונטייר כניסה למשתמש רשום במערכת
        $("#emailSignInDiv").hide();
    }

    if (ContainerID == "01") {
        $("#emailSignUpDiv").hide();
        $("#profileContainer").show();  //הצגת הקונטייר כניסה למשתמש רשום במערכת
        $("#emailSignInDiv").show();  //הצגת הקונטייר כניסה למשתמש רשום במערכת
        $("#proflieNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט
        $("#profileLinkButton").css({ "pointer-events": "auto", "opacity": "1" }); //החזרת פרופיל בתפריט ניווט ללינק פעיל
    }
    else if (ContainerID == "1") {
        $("#howtoplayContainer").show(); //הצגת קונטיינר איך משחקים
    }

    else if (ContainerID == "2") {
        $("#preGameContainer").show();  //הצגת הקונטייר של לפני המשחק- בחירת קטגוריה
    }

    else if (ContainerID == "GameStarted") {
        $(".gameBtns").prop('disabled', true);//הגדרת כפתורי המשחק למצב לא פעיל
        $("#gameContainer").show(); //הצגת קונטיינר שח המשחק עצמוeach
        $("#gameNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט


        //הגדרת טיימר למשחק
        var elem = document.getElementById('CountDownLabel');
        timerId = setInterval(function () {
            if (!isPaused) {

                if (timeLeft == 0) { //במידה ונגמר הזמן
                    clearTimeout(timerId);
                    $("#StarIcon3").css('opacity', '0.3');
                    $("#TimeEndContainer").show();
                    $("#gameContainer").hide();
                }
                else if (20 >= timeLeft && timeLeft >= 11) { //במידה ונשאר בין 11-20 שניות
                    $("#StarIcon1").css('opacity', '0.3');
                    console.log("timeLeft: " + timeLeft);
                    elem.innerHTML = timeLeft;
                    timeLeft--;
                }
                else if (10 >= timeLeft) { //במידה ונשאר בין 1-10 שניות
                    $("#StarIcon2").css('opacity', '0.3');
                    console.log("timeLeft: " + timeLeft);
                    elem.innerHTML = timeLeft;
                    timeLeft--;
                }
                else {
                    elem.innerHTML = timeLeft; //הדפסת מספר השניות שנותרו
                    timeLeft--;
                }

            }
        }, 1000);

    }
    else if (ContainerID == "GameFeedback") {
        $("#feedbackContainer").show(); //הצגת קונטיינר משוב לשאלה   
        $("#gameNavImage").css({ "height": "35px", "width": "35px" }); //הגדלת אייקון הפרופיל בתפריט ניווט
    }
    else if (ContainerID == "3") {  //הצגת קונטיינר תוצאות
        $("#scoreContainer").show();
        loadCharts();
    }
    else if (ContainerID == "4") {
        $("#moreContainer").show();  //הצגת הקונטייר של עוד
    }



    //כאשר מתבצע לחיצה על אחד מכפתורי הניווט
    $('.navBtn').click(function () {

        //הוצאת מספר הקונטיינר שנלחץ
        var conNum = $(this).attr("containernum");
        document.getElementById('NavHiddenField').value = conNum;

    });

    $('#chooseCatDDL').change(function () {
        $('#catFinishedLabel').text('');
        console.log("returning false");
        return false;
    });

    //לחיצה על כפתור התחל משחק - לאחר שקטגוריה נבחרה
    $('#startGameButton').click(function () {

        document.getElementById('NavHiddenField').value = "GameStarted";
   
    });

    //לחיצה על כפתור המשך למשחק (לאחר איך משחקים)
    $('#navToGameBtn').click(function () {
        document.getElementById('NavHiddenField').value = "2";
    });

    //לחיצה על כפתור המשך למשחק (לאחר איך משחקים)
    $('#navToGameLink').click(function () {
        document.getElementById('NavHiddenField').value = "2";
    });

    //לחיצה על אחד מכפתורי המשחק - חיוני או לא חיוני
    $('.gameBtns').click(function () {

        document.getElementById('NavHiddenField').value = "GameFeedback";

        //הכנסת ערך של מספר שניות שלקח למשתמש כדי לענות על מקרה אל תוך שדה מוחבא
        var curremtTime = (30 - timeLeft);
        document.getElementById('SecondsHiddenField').value = curremtTime;

    });

    //לחיצה על כפתור היכנס בתור משתמש רשום (בפרופיל)
    $('#SignInLink').click(function () {
        $("#emailTB1").val("");
        $("#emailTB").val("");
        document.getElementById('NavHiddenField').value = "01";

    });

    //לחיצה על כפתור הירשם בתור משתמש חדש (בפרופיל)
    $('#SignUpLink').click(function () {
        $("#fullNameTB").val("");
        document.getElementById('NavHiddenField').value = "0";
    });

    var swiper = new Swiper('#mySwiper', {
        spaceBetween: 30,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
    });


    swiper.on('slideChange', function () {
        if (swiper.isEnd) {
            console.log("end is reached");

            $(".gameBtns").prop('disabled', false);//הגדרת כפתורי המשחק למצב לא פעיל
            endReached = true;
        }
    });



    var howtoplayswiper = new Swiper('#howtoplaySwiper', {
        spaceBetween: 30,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
    });


    howtoplayswiper.on('slideChange', function () {
        if (howtoplayswiper.isEnd) {
            console.log("end is reached");

            $('#navToGameLink').css("display", "none");
            $('#navToGameBtn').css("display", "block");
        }
    });


    //לחיצה על כפתור השהייה במשחק
    $('#PauseImageButton').click(function () {

        if (isPaused == false) {

            $(".gameBtns").prop('disabled', true);//הגדרת כפתורי המשחק למצב לא פעיל
            $("#mySwiper").hide(); //הסתרת התוכן של פרטי המטופל
            $("#ContinueLinkButton").css('display', 'block'); //הצגת כיתוב המאפשר לחזור למשחק
            $("#timerDiv").css("background-color", "rgb(244, 244, 244)");

            this.src = "images/play.svg"; //שינוי התמונה לאייקון המשך משחק

            isPaused = true;
        }
        else {

            if (endReached == true) {
                $(".gameBtns").prop('disabled', false);//הגדרת כפתורי המשחק למצב פעיל
            }

            $("#timerDiv").css("background-color", "rgba(169, 218, 220, 0.3)");
            $("#mySwiper").show(); //הצגת התוכן של פרטי המטופל
            $("#ContinueLinkButton").hide();//הצגת כיתוב המאפשר לחזור למשחק

            this.src = "images/pause.svg"; //שינוי התמונה לאייקון שהייה

            isPaused = false;
        }

        return false;

    });

    //כפתור ביטול כפתור ההשהייה - המשך במשחק
    $('#ContinueLinkButton').click(function () {

        $('#PauseImageButton').click();

        return false;
    });

    $('.tempNA').click(function () {

        $('#tempNote').text("כפתור זה אינו עובד באופן זמני.");

        return false;
    });



    $('#profileBottomDiv').click(function () {

        $('#UserLoggedInHiddenField').val("false");
        $("#SavedLabel").css("display", "none");
        document.getElementById('NavHiddenField').value = "01";


    });

    $('#aboutBtn').click(function () {

        $('#aboutDiv').css("display", "block");

        return false;

    });

    $('#aboutExitBtn').click(function () {

        $('#aboutDiv').css("display", "none");

        $('#tempNote').text("");

        return false;

    });


    $('#departmentDD').click(function () {

        $("#SavedLabel").css("display", "none");

    });

    //-------------------------------------------------ספירת תווים------------------------------//

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        $("#SavedLabel").css("display", "none");
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });

    checkCharacter($(".CharacterCount"));

    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {

        //משתנה המקבל את מספר תיבת הטקסט 
        var connectBtnAttr = myTextBox.attr("connectBtn");

        //משתנה ששומר את מספר תיבות הטקסט שמחוברות לאותו הכפתור
        var TextBoxsCounter = 0;

        //משתנה ששומר את מספר התיבות טקסט שיש בתוכם לפחות תו אחד
        var TextBoxsLength = 0;

        //פונקציה שעוברת על כל הרכיבים עם קלאס CharacterCount
        $(".CharacterCount").each(function () {
            if ($(this).attr("connectBtn") == connectBtnAttr) {
                TextBoxsCounter++; //ספירת תיבות הטקסט שמחוברת לאותו הכפתור

                if ($(this).val().length > 0) {
                    TextBoxsLength++; //ספירת תיבות הטקסט שבהם יש תו אחד לפחות
                }

            }
        });

        //תנאי שבודק אם מספר התיבות טקסט זהה מספר התיבות טקסט שיש בהם לפחות תו אחד
        if (TextBoxsCounter == TextBoxsLength) {
            document.getElementById(myTextBox.attr("connectBtn")).disabled = false; //הפיכת הכפתור למצב פעיל
        }
        else {
            document.getElementById(myTextBox.attr("connectBtn")).disabled = true; //הפיכת הכפתור למצב לא פעיל
        }

    }

    //-----------------------------העלאת תמונות--------------------------------//

    function isUploadSupported() {
        if (navigator.userAgent.match(/(Android (1.0|1.1|1.5|1.6|2.0|2.1))|(Windows Phone (OS 7|8.0))|(XBLWP)|(ZuneWP)|(w(eb)?OSBrowser)|(webOS)|(Kindle\/(1.0|2.0|2.5|3.0))/)) {
            return false;
        }
        var elem = document.createElement('input');
        elem.type = 'file';
        return !elem.disabled;
    };


    $("#fileUpload").change(function () {

        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#SavePic1').click();
            }
            reader.readAsDataURL(this.files[0]);
        }

        $('#UserLoggedInHiddenField').val("true");
    });

    $("#SavePic1").click(function () {

        $('#UserLoggedInHiddenField').val("true");
        $("#SavedLabel").css("display", "block");

    });

    $("#profilePicBtn").click(function () {

        $("#SavedLabel").css("display", "none");

    });

    $("#addPicHyperLink").click(function () {

        $("#SavedLabel").css("display", "none");

    });




});

//-----------------------------העלאת תמונות--------------------------------//


function openFileUploader1() {
    $('#fileUpload').click();
}



//-----------------------------הטענת גרפים לעמוד תוצאות--------------------------------//

function loadCharts() {

    var ctx = document.getElementById("myChart").getContext('2d');
    var config = createConfig();
    new Chart(ctx, config);

}



function createConfig() {

    var CorrectCases = $("#CorrectCasesHiddenField").val();
    var incorrectCases = $("#IncorrectCasesHiddenField").val();
    var sumCases = $("#CasesCountHiddenField").val();
    var leftCase = (sumCases - CorrectCases - incorrectCases);

    return {
        type: 'doughnut',
        data: {
            labels: ['תשובות נכונות', 'תשובות לא נכונות', 'תשובות שעוד לא נענו'],
            datasets: [{
                data: [CorrectCases, incorrectCases, leftCase],
                backgroundColor: ['#87cefa',
                    '#6495ed',
                    '#8a2be2'],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            legend: {
                position: 'bottom',
                rtl: true,
                textDirection: 'rtl',
                labels: {
                    defaultFontSize: 16,
                    boxWidth: 15,
                    padding: 10,
                    fontSize: 16,
                    fontFamily: 'Heebo',
                    fontColor: 'black'
                }
            },
            tooltips: {
                rtl: true,
                textDirection: 'rtl',
            },
            scales: {
                xAxes: [{
                    display: false,
                }],
                yAxes: [{
                    display: false
                }]
            },
            title: {
                display: false,
                text: 'מענה על סיפורי מקרה'
            },
            aspectRatio: 1
        }
    };
}


//-----------------------------קוד שאחראי לפעולת החלקה swipe--------------------------------//


// TOUCH-EVENTS SINGLE-FINGER SWIPE-SENSING JAVASCRIPT
// Courtesy of PADILICIOUS.COM and MACOSXAUTOMATION.COM

// this script can be used with one or more page elements to perform actions based on them being swiped with a single finger

var triggerElementID = null; // this variable is used to identity the triggering element
var fingerCount = 0;
var startX = 0;
var startY = 0;
var curX = 0;
var curY = 0;
var deltaX = 0;
var deltaY = 0;
var horzDiff = 0;
var vertDiff = 0;
var minLength = 72; // the shortest distance the user may swipe
var swipeLength = 0;
var swipeAngle = null;
var swipeDirection = null;


function touchStart(event, passedName) {
    // disable the standard ability to select the touched object
    //event.preventDefault();
    // get the total number of fingers touching the screen
    fingerCount = event.touches.length;
    // since we're looking for a swipe (single finger) and not a gesture (multiple fingers),
    // check that only one finger was used
    if (fingerCount == 1) {

        // get the coordinates of the touch
        startX = event.touches[0].pageX;
        startY = event.touches[0].pageY;
        // store the triggering element ID
        triggerElementID = passedName;
    } else {
        // more than one finger touched so cancel
        //touchCancel(event);
    }
}

function touchMove(event) {

    console.log("touchmove");
    event.preventDefault(); //ניסיון למנוע מהדפדפן לעבור בין עמודים שונים בבראוזר


    if (event.touches.length == 1 && !isPaused && endReached == true) {
        curX = event.touches[0].pageX;
        curY = event.touches[0].pageY;
        if (fingerCount == 1) {
            // use the Distance Formula to determine the length of the swipe
            swipeLength = Math.round(Math.sqrt(Math.pow(curX - startX, 2) + Math.pow(curY - startY, 2)));
            console.log("one finger pressed");

            //שליפת מיקום של גלרית פרטי המטופל
            var rect = document.getElementById("mySwiper").getBoundingClientRect();

            // if the user swiped more than the minimum length, perform the appropriate action
            if ((swipeLength >= minLength) && ((startY > (rect.bottom + 10)) || (startY < (rect.top - 10)))) {
                console.log("swipe more than minimum");
                console.log("swipe length: " + swipeLength);


                //קריאה לפונקציה המחשבת את כיוון הswipe
                caluculateAngle();
                determineSwipeDirection();


                event.preventDefault();


                var directionImg;

                //תנאי הבודק לאיזה כיוון המשתמש החליק
                if (swipeDirection == 'left') {

                    directionImg = $('#dontsendImg');

                    //לשנות צבעי הרקע לאפור
                    $('#sendBtn').prop('disabled', true);

                }

                if (swipeDirection == 'right') {

                    directionImg = $('#sendImg');

                    //לשנות צבעי הרקע לאפור
                    $('#dontSendBtn').prop('disabled', true);

                }



                directionImg.show();
                directionImg.css("opacity", 0.2);
                directionImg.css("width", "70%");


                if (swipeLength >= 100) {
                    directionImg.css("opacity", 0.7);
                    directionImg.css("width", "80%");
                }

                if (swipeLength >= 150) {
                    directionImg.css("opacity", 1);
                    directionImg.css("width", "90%");
                }
                directionImg.show();




            }
        }

    } else {
        touchCancel(event);
    }
}

function touchEnd(event) {
    //event.preventDefault();
    // check to see if more than one finger was used and that there is an ending coordinate
    if (fingerCount == 1 && curX != 0) {
        // use the Distance Formula to determine the length of the swipe
        swipeLength = Math.round(Math.sqrt(Math.pow(curX - startX, 2) + Math.pow(curY - startY, 2)));


        var rect = document.getElementById("mySwiper").getBoundingClientRect();
        console.log("top: " + rect.top + "right: " + rect.right + "bottom: " + rect.bottom + "left: " + rect.left);

        //החבאת תמונות לאחר סיום לחיצה
        $('#sendImg').hide();
        $('#dontsendImg').hide();

        //להחזיר את צבע הכפתורים לקדמותם
        // $('#sendBtn').css({ "background-color": "white !important", "color": "blue !important", "box-shadow": "0px 3px 20px #c5c5c5 !important" });
        $('#sendBtn').prop('disabled', false);
        $('#dontSendBtn').prop('disabled', false);


        // if the user swiped more than the minimum length, perform the appropriate action
        if ((swipeLength >= minLength) && ((startY > (rect.bottom + 10)) || (startY < (rect.top - 10)))) {
            caluculateAngle();
            determineSwipeDirection();
            processingRoutine();
            touchCancel(event); // reset the variables


        } else {
            touchCancel(event);
        }
    } else {
        touchCancel(event);
    }
}

function touchCancel(event) {
    // reset the variables back to default values
    fingerCount = 0;
    startX = 0;
    startY = 0;
    curX = 0;
    curY = 0;
    deltaX = 0;
    deltaY = 0;
    horzDiff = 0;
    vertDiff = 0;
    swipeLength = 0;
    swipeAngle = null;
    swipeDirection = null;
    triggerElementID = null;
}

function caluculateAngle() {
    var X = startX - curX;
    var Y = curY - startY;
    var Z = Math.round(Math.sqrt(Math.pow(X, 2) + Math.pow(Y, 2))); //the distance - rounded - in pixels
    var r = Math.atan2(Y, X); //angle in radians (Cartesian system)
    swipeAngle = Math.round(r * 180 / Math.PI); //angle in degrees
    if (swipeAngle < 0) { swipeAngle = 360 - Math.abs(swipeAngle); }
}

function determineSwipeDirection() {
    if ((swipeAngle <= 45) && (swipeAngle >= 0)) {
        swipeDirection = 'left';
    } else if ((swipeAngle <= 360) && (swipeAngle >= 315)) {
        swipeDirection = 'left';
    } else if ((swipeAngle >= 135) && (swipeAngle <= 225)) {
        swipeDirection = 'right';
    } else if ((swipeAngle > 45) && (swipeAngle < 135)) {
        swipeDirection = 'down';
    } else {
        swipeDirection = 'up';
    }
}

function processingRoutine() {
    var swipedElement = document.getElementById(triggerElementID);
    if (swipeDirection == 'left') {
        console.log("swiped");
        $('#dontSendBtn').click();


    } else if (swipeDirection == 'right') {
        console.log("swiped");
        $('#sendBtn').click();

    } else if (swipeDirection == 'up') {
        return false;
        console.log("swiped");

    } else if (swipeDirection == 'down') {
        return false;
        console.log("swiped");
    }
}