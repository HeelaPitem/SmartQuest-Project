var saveAllowed = true;

$(document).ready(function () {


    //-----------------------------צד עורך--------------------------------//
    console.log("loaded");



    $(".caseGridView").each(function () {

        $(this).hide();

        var SelectedTest = document.getElementById('SelectedTestHF').value;

        if (SelectedTest != null) {

            $("#caseGridView" + SelectedTest + "").show();

        }

    });

    $(".noCasesLabel").each(function () {
        $(this).hide();
    });





    $(".testLinks").click(function (e) {

        //הוצאת מספר מזהה של הכפתור שנלחץ
        var btnId = $(this).attr("ID");
        var testId = btnId.slice(8);

        var gv = $("#caseGridView" + testId + "");

        if (gv.length > 0) {
            $("#caseGridView" + testId + "").toggle();
        }
        else {
            $("#noCasesLabel" + testId + "").toggle();
        }

        console.log("#caseGridView" + testId);

        document.getElementById('SelectedTestHF').value = testId;

        console.log("SelectedTest: " + testId);

        e.preventDefault();

    });



    $('#confnNoDelete').click(function () {

        $("#grayWindows0").hide();

    });



    $('.editIcons').click(function () {

        //הוצאת מספר מזהה של הכפתור שנלחץ
        var btnId = $(this).attr("ID");
        var testId = btnId.slice(7);

        $("#editTestName" + testId + "").css("display", "inline-block");
        $("#saveNameImg" + testId + "").css("display", "block");

        $("#testLink" + testId + "").hide();
        $("#editImg" + testId + "").hide();

        return false;

    });


    $("#answerRBL").change(function () {

        if (saveAllowed == true) {
            document.getElementById("saveBtn").disabled = false;
        }

    });



    $(".CharacterCount").each(function () {

        var countCurrentC = $(this).val().length;

        if (countCurrentC > 0) {


            //משתנה המקבל את מספר תיבת הטקסט 
            var itemNumber = $(this).attr("item");

            if (itemNumber != null) {

                //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
                var CharacterLimitNum = $(this).attr("CharacterLimit");

                $("#LabelCounter" + itemNumber).text(countCurrentC + "/" + CharacterLimitNum);
                document.getElementById("LabelCounter" + itemNumber).style.color = "green";

            }


        }

    });

    //-------------------------------------------------ספירת תווים------------------------------//

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });


    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {

        //משתנה המקבל את מספר תיבת הטקסט 
        var connectBtnAttr = myTextBox.attr("connectBtn");

        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;

        //משתנה המקבל את מספר תיבת הטקסט 
        var itemNumber = myTextBox.attr("item");

        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimitNum = myTextBox.attr("CharacterLimit");

        //משתנה ששומר את מספר תיבות הטקסט שמחוברות לאותו הכפתור
        var TextBoxsCounter = 0;

        //משתנה ששומר את מספר התיבות טקסט שיש בתוכם לפחות תו אחד
        var TextBoxsLength = 0;




        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC > CharacterLimitNum) {

            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, CharacterLimitNum));
            //עדכון של מספר התווים הנוכחי
            countCurrentC = CharacterLimitNum;
            document.getElementById("LabelCounter" + itemNumber).style.color = "red";

        }


        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#LabelCounter" + itemNumber).text(countCurrentC + "/" + CharacterLimitNum);

        //פונקציה שעוברת על כל הרכיבים עם קלאס CharacterCount
        $(".CharacterCount").each(function () {

            if ($(this).attr("connectBtn") == "loginBtn") {
                TextBoxsCounter++; //ספירת תיבות הטקסט שמחוברת לאותו הכפתור

                if ($(this).val().length > 0) {
                    TextBoxsLength++; //ספירת תיבות הטקסט שבהם יש תו אחד לפחות
                }

            }
        });



        if (myTextBox.hasClass("contentTB")) {

            var TBCounter = 0;
            var TBLength = 0;

            $(".CharacterCount").filter('.contentTB').each(function () {

                if ($(this).val().length > 0) {
                    TBLength++; //ספירת תיבות הטקסט שבהם יש תו אחד לפחות
                }

            });



            if (TBLength >= 2) {
                document.getElementById(connectBtnAttr).disabled = false;
            }
            else {
                document.getElementById(connectBtnAttr).disabled = true;
                saveAllowed = false;
            }

            var numEnabledPanels = document.getElementById('numofCasesShownHF').value;

            if (numEnabledPanels == TBLength) {

                numEnabledPanels++;

                $("#patientPanel" + numEnabledPanels).addClass('activepatientPanel').removeClass('patientPanel');

                $("#titleTB" + numEnabledPanels).removeClass('grayborderbottom');
                $("#titleTB" + numEnabledPanels + "").attr("readonly", false);

                $("#contentTB" + numEnabledPanels).removeClass('cursornotallowed');
                $("#contentTB" + numEnabledPanels + "").attr("readonly", false);

                $("#numberLabel" + numEnabledPanels).addClass('numbersLabel').removeClass('grayfont');


                document.getElementById('numofCasesShownHF').value = numEnabledPanels;
            }


            var tbId = myTextBox.attr("ID");
            var panelId = tbId.slice(9);


            if (countCurrentC == 0) {

                $("#numberLabel" + panelId).addClass('grayfont').removeClass('numbersLabel');
            }
            else {

                $("#numberLabel" + panelId).addClass('numbersLabel').removeClass('grayfont');
            }


        }





        if (countCurrentC > 0) { //אם יש תו אחד לפחות בתיבת טקסט




            if (document.getElementById('changesMadeHF') != null) {
                document.getElementById('changesMadeHF').value = "true";
            }

            if (document.getElementById(connectBtnAttr) != null) {

                if (connectBtnAttr == "loginBtn") {
                    $("#loginLabel").html("");
                }
                else {
                    if (saveAllowed == true) {
                        document.getElementById(connectBtnAttr).disabled = false;
                    }
                }


            }


            //תנאי שבודק אם מספר התיבות טקסט זהה מספר התיבות טקסט שיש בהם לפחות תו אחד
            if (connectBtnAttr == "loginBtn" && TextBoxsCounter == TextBoxsLength) {
                document.getElementById(connectBtnAttr).disabled = false;
            }


            if (itemNumber != null) {
                document.getElementById("LabelCounter" + itemNumber).style.color = "green";
            }

        } else { //אם אין תווים בתיבת טקסט


            if (connectBtnAttr == "addTestBtn" || connectBtnAttr == "loginBtn") {
                document.getElementById(connectBtnAttr).disabled = true;
            }

            if (itemNumber != null) {
                $("#LabelCounter" + itemNumber).text(countCurrentC + "/" + CharacterLimitNum);
                document.getElementById("LabelCounter" + itemNumber).style.color = "black";
            }

        }

        if (countCurrentC == CharacterLimitNum) {
            if (itemNumber != null) {
                document.getElementById("LabelCounter" + itemNumber).style.color = "red";
            }
        }


    }




});
