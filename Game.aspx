<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Smart-Quest | משחק</title>

    <meta name="description" content="משחק עבור מתמחים בבית חולים רמבם במסגרת מיזם Smart-Med" />
    <meta name="keywords" content="משחק, למידה, תרגול, בדיקות, בדיקות רפואיות, מתמחים, מתמחה, רופאים, רופא, בית חולים, רמבם, סיפורי מקרה" />
    <meta name="author" content="הילה פיטם ושירה פיקר" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=yes" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <%--CSS--%>
    <link href="Styles/reset.css" rel="stylesheet" type="text/css" />
    <link href="Styles/myStyle.css" rel="stylesheet" type="text/css" />
    <link href="https://unpkg.com/swiper/swiper-bundle.css" rel="stylesheet" />
    <link href="https://unpkg.com/swiper/swiper-bundle.min.css" rel="stylesheet" />
    <link rel="icon" type="image/png" href="/images/favicon.png" />

    <%--Scripts--%>
    <script src="https://unpkg.com/swiper/swiper-bundle.js"></script>
    <script src="jScripts/Chart.bundle.min.js"></script>
    <script src='http://code.jquery.com/jquery-3.3.1.slim.min.js'></script>
    <script src="jscripts/JavaScript.js"></script>

</head>
<body>


    <form id="form1" runat="server">

        <header>

            <nav id="gameTopLogo" class="logoContainer" runat="server">
                <asp:Image ID="headerImage" ImageUrl="~/images/smartquestlogo.png" runat="server" />
            </nav>

        </header>


        <div id="wrapContentDiv">



            <%-------------- עמוד פרופיל---------------%>

            <div id="profileContainer" class="container" containerid="0" runat="server">

                <asp:Button ID="SavePic1" CssClass="displayNone" runat="server" Text="Button" OnClick="SavePic1_Click" />
                <input id="fileUpload" type="file" class="displayNone" accept="image/*" runat="server" />

                <asp:Label ID="SavedLabel" runat="server" Text="שינויים נשמרו בהצלחה"></asp:Label>

                <div id="profileTopDiv" runat="server">
                    <asp:Image ID="longlogoImg" runat="server" ImageUrl="~/images/longlogo.png" Width="200px" />
                    <asp:ImageButton ID="profilePicBtn" CssClass="profilePicBtn" runat="server" OnClientClick="openFileUploader1(); return false;" />
                    <asp:LinkButton ID="addPicHyperLink" runat="server" OnClientClick="openFileUploader1(); return false;"></asp:LinkButton>

                </div>


                <%-------------- דיב פנימי לכניסה למשתמשים חדשים---------------%>

                <div id="emailSignUpDiv" class="userInfoInput container">


                    <label for="fullNameTB">שם מלא</label>

                    <asp:TextBox ID="fullNameTB" CssClass="CharacterCount" connectBtn="SignUpBtn" runat="server" OnTextChanged="fullNameTB_TextChanged"></asp:TextBox>

                    <br />

                    <label for="emailTB" id="emailLabel">מייל</label>

                    <asp:TextBox ID="emailTB" CssClass="CharacterCount" connectBtn="SignUpBtn" runat="server" type="email" OnTextChanged="emailTB_TextChanged"></asp:TextBox>
                    <br />
                    <asp:Label ID="infoLabel" runat="server" Text="*לצורך זיהוי בלבד"></asp:Label>
                    <br />


                    <label for="departmentTB">מחלקה</label>

                    <asp:DropDownList ID="departmentDD" runat="server" OnSelectedIndexChanged="departmentDD_SelectedIndexChanged"></asp:DropDownList>

                    <asp:Button ID="SignUpBtn" CssClass="actionBtns" runat="server" Text="כניסה" OnClick="SignUpBtn_Click" disabled="true" />

                    <asp:LinkButton ID="SignInLink" Text="שיחקת כבר בעבר? היכנס כאן." runat="server" />

                    <asp:LinkButton ID="profileBottomDiv" runat="server" OnClick="profileBottomDiv_Click">
                        <asp:Image ID="LogOutImage" runat="server" ImageUrl="~/images/logout.svg" />
                        <asp:Label runat="server">לא הפרטים שלך? החלף משתמש</asp:Label>
                    </asp:LinkButton>

                </div>





                <%-------------- דיב פנימי לכניסה למשתמשים קיימים---------------%>

                <div id="emailSignInDiv" class="userInfoInput container">


                    <div id="innerSignInDiv">
                        <p class="instructions">הזן כתובת מייל על מנת להיכנס למשחק.</p>

                        <asp:TextBox ID="emailTB1" runat="server" CssClass="CharacterCount" connectBtn="SignInBtn" type="email"></asp:TextBox>

                        <asp:Label ID="infoLabel1" runat="server" Text="*לצורך זיהוי בלבד"></asp:Label>

                    </div>


                    <asp:Button ID="SignInBtn" CssClass="actionBtns" runat="server" Text="כניסה" OnClick="SignInBtn_Click" disabled="true" />


                    <asp:LinkButton ID="SignUpLink" Text="משתמש חדש? הירשם כאן." runat="server" />


                </div>






            </div>




            <%-------------- עמוד איך משחקים---------------%>


            <div id="howtoplayContainer" class="container" containerid="1" runat="server">

                <label id="welcomeLabel" runat="server" />

                <h3>איך משחקים</h3>

                <div id="howtoplaySwiper" class="swiper-container">

                    <div class="swiper-wrapper howtoplayWrapper" runat="server">




                        <asp:Panel CssClass="swiper-slide" runat="server">

                            <asp:Image CssClass="howtoplayImg" runat="server" ImageUrl="~/images/gif1.gif" />

                            <br />

                            <asp:Panel CssClass="HTPtextPanel" runat="server">

                                <asp:Label Text="בחר בדיקה מתוך המאגר ולחץ על כפתור התחל במשחק." runat="server" />

                            </asp:Panel>

                        </asp:Panel>



                        <asp:Panel CssClass="swiper-slide" runat="server">

                            <asp:Image CssClass="howtoplayImg" runat="server" ImageUrl="~/images/gif2.gif" />

                            <br />

                            <asp:Panel CssClass="HTPtextPanel" runat="server">

                                <asp:Label Text="החלק ימינה או שמאלה בתיבה כדי לחשוף את פרטי המטופל." runat="server" />

                            </asp:Panel>

                        </asp:Panel>






                        <asp:Panel CssClass="swiper-slide" runat="server">

                            <asp:Image CssClass="howtoplayImg" runat="server" ImageUrl="~/images/gif3.gif" />

                            <br />

                            <asp:Panel CssClass="HTPlongtextPanel" runat="server">

                                <asp:Label Text="החלק על המסך או לחץ על כפתורי המשחק על מנת לקבוע האם הבדיקה חיונית או לא במקרה זה." runat="server" />

                            </asp:Panel>

                        </asp:Panel>




                        <asp:Panel CssClass="swiper-slide" runat="server">

                            <asp:Image CssClass="howtoplayImg" runat="server" ImageUrl="~/images/gif4.gif" />

                            <br />

                            <asp:Panel CssClass="HTPlongtextPanel" runat="server">

                                <asp:Label Text="ישנה הגבלת זמן של 30 שניות לתגובה. מענה נכון תזכה את מחלקתך ב1-3 נקודות בהתאם למהירות תגובתך." runat="server" />

                            </asp:Panel>

                        </asp:Panel>





                    </div>

                    <!-- Add Pagination -->
                    <div class="swiper-pagination"></div>

                </div>


                <asp:Button ID="navToGameBtn" CssClass="actionBtns" runat="server" Text="המשך למשחק" />

                <asp:LinkButton ID="navToGameLink" runat="server">דלג למשחק</asp:LinkButton>

            </div>



            <%-------------- עמוד בחירת קטגוריה---------------%>


            <div id="preGameContainer" class="container" containerid="2" runat="server">

                <asp:Label ID="catFinishedLabel" runat="server" />

                <asp:Label ID="chooseCatgLabel" runat="server">איזו בדיקה תרצה לתרגל?</asp:Label>

                <asp:DropDownList ID="chooseCatDDL" runat="server" OnSelectedIndexChanged="chooseCat_OnSelectedIndexChanged">
                </asp:DropDownList>

                <asp:Button ID="startGameButton" CssClass="actionBtns" runat="server" Text="התחל משחק" OnClick="startGameButton_Click" />

                <asp:HiddenField ID="NavHiddenField" runat="server" />
                <asp:HiddenField ID="SecondsHiddenField" runat="server" />
                <asp:HiddenField ID="UserLoggedInHiddenField" runat="server" />
                <asp:HiddenField ID="CorrectCasesHiddenField" runat="server" />
                <asp:HiddenField ID="IncorrectCasesHiddenField" runat="server" />
                <asp:HiddenField ID="CasesCountHiddenField" runat="server" />



            </div>


            <%-------------- עמוד המשחק---------------%>

            <div id="gameContainer" class="container" containerid="2" runat="server">


                <div id="timerDiv">

                    <asp:Label ID="CountDownLabel" runat="server" Text="30"></asp:Label>
                    <asp:ImageButton ID="PauseImageButton" CssClass="icons" runat="server" ImageUrl="~/images/pause.svg" />

                    <div id="starImagesDiv">
                        <asp:Image ID="StarIcon1" class="starIcons" runat="server" ImageUrl="~/images/star.svg" />
                        <asp:Image ID="StarIcon2" class="starIcons" runat="server" ImageUrl="~/images/star.svg" />
                        <asp:Image ID="StarIcon3" class="starIcons" runat="server" ImageUrl="~/images/star.svg" />
                    </div>

                </div>




                <div id="bgSwiperDiv" runat="server"></div>

                <div id="innerGameDiv" runat="server" ontouchstart="touchStart(event,'bgSwiperDiv');" ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">

                    <asp:ImageButton ID="CategorybackBtn" CssClass="icons" runat="server" ImageUrl="~/images/logout.svg" OnClick="CategorybackBtn_Click" />


                    <div id="categoryDiv" runat="server">
                        <h3 id="CategoryName" runat="server"></h3>
                        <asp:Label ID="allCatLabel" runat="server"></asp:Label>
                    </div>

                    <asp:LinkButton ID="ContinueLinkButton" runat="server" Text="לחץ כאן כדי להמשיך לשחק." CauseValidation="false"></asp:LinkButton>


                    <!-- Swiper -->

                    <div id="patiantDetailsDiv">

                        <div id="mySwiper" class="swiper-container">

                            <div class="swiper-wrapper" id="swipperwrapperid" runat="server"></div>
                            <!-- Add Pagination -->
                            <div class="swiper-pagination"></div>

                        </div>

                    </div>


                    <asp:Image ID="sendImg" runat="server" ImageUrl="~/images/approved.png" />
                    <asp:Image ID="dontsendImg" runat="server" ImageUrl="~/images/dismissed.png" />


                    <asp:Panel ID="buttonsPanel" runat="server">

                        <asp:Button ID="sendBtn" CssClass="gameBtns" runat="server" Text="חיוני" OnClick="GameBtns_Click" />
                        <asp:Button ID="dontSendBtn" CssClass="gameBtns" runat="server" Text="לא חיוני" OnClick="GameBtns_Click" />
                    </asp:Panel>

                </div>

            </div>





            <%-------------- עמוד נגמר הזמן---------------%>

            <div id="TimeEndContainer" class="container" containerid="2" runat="server">

                <asp:Label ID="OutOfTimeLabel" runat="server" Text="נגמר הזמן"></asp:Label>

                <asp:Button ID="ContinueGame" CssClass="actionBtns" runat="server" Text="המשך משחק" OnClick="startGameButton_Click" />


            </div>



            <%-------------- עמוד משוב---------------%>

            <div id="feedbackContainer" class="container" containerid="2" runat="server">
                <asp:Label ID="feedbackTitle" runat="server"></asp:Label>

                <asp:Label ID="feedbackExp" runat="server"></asp:Label>

                <asp:Label ID="TimeLabel" runat="server"></asp:Label>



                <asp:Panel ID="departmentsDiv" runat="server">
                    <h3>ניקוד לפי מחלקה</h3>
                    <div id="testDiv" runat="server"></div>
                </asp:Panel>


                <asp:Button ID="nextqBtn" CssClass="actionBtns" runat="server" Text="שאלה הבאה" OnClick="startGameButton_Click" />
            </div>




            <%-------------- עמוד תוצאות---------------%>

            <div id="scoreContainer" class="container" containerid="3" runat="server">

                <h3>איזור אישי</h3>

                <div id="topScoreBar" runat="server">


                    <div id="rightsideResults" runat="server">

                        <div id="pointsDiv" class="resultsDiv">
                            <asp:Label ID="pointsLabel1" runat="server" Text="הוספת למחלקתך"></asp:Label>
                            <asp:Image ID="starImg" class="scoreIcons" runat="server" ImageUrl="~/images/star.svg" />
                            <label id="pointsLabel2" runat="server"></label>
                        </div>


                        <div id="avgSecondsDiv" class="resultsDiv">
                            <asp:Label ID="avgSecLabel1" runat="server" Text="זמן ממוצע למענה"></asp:Label>
                            <asp:Image ID="avgSecondsIcon" class="scoreIcons" runat="server" ImageUrl="~/images/hourglass.svg" />
                            <label id="avgSecLabel2" runat="server"></label>
                        </div>


                    </div>


                    <div id="userAnsweredDiv" class="resultsDiv" runat="server">
                        <p class="charts">מענה על סיפורי המקרה</p>
                        <canvas id="myChart" class="charts"></canvas>
                    </div>

                </div>




                <h3>הרופאים המובילים</h3>
                <div id="topDoctorsDiv" class="resultsDiv" runat="server">
                    <div id="docDiv3" class="docDivs" runat="server">
                        <asp:Image ID="topDocImage3" class="topDocsImage" runat="server" />
                        <asp:Label class="topDocsPlace" runat="server" Text="3"></asp:Label>
                        <asp:Label ID="topDoc3Label" class="topDocLabel" runat="server"></asp:Label>
                    </div>
                    <div id="docDiv1" class="docDivs" runat="server">
                        <asp:Image ID="topDocImage1" class="topDocsImage" runat="server" />
                        <asp:Label class="topDocsPlace" runat="server" Text="1"></asp:Label>
                        <asp:Label ID="topDoc1Label" class="topDocLabel" runat="server"></asp:Label>
                    </div>
                    <div id="docDiv2" class="docDivs" runat="server">
                        <asp:Image ID="topDocImage2" class="topDocsImage" runat="server" />
                        <asp:Label class="topDocsPlace" runat="server" Text="2"></asp:Label>
                        <asp:Label ID="topDoc2Label" class="topDocLabel" runat="server"></asp:Label>
                    </div>

                </div>

                <h3>ניקוד מחלקתי</h3>

                <div id="depScoresDiv" class="resultsDiv" runat="server">
                </div>


            </div>

            <%-------------- עמוד אודות---------------%>


            <div id="moreContainer" class="container" containerid="4" runat="server">

                <div id="aboutDiv">

                    <h3>אודות</h3>

                    <h4>Smart-Quest</h4>
                    <p class="gameExp">משחק חוויתי למתמחים במסגרת מיזם Smart-Med.</p>

                    <div id="innerAbout">

                        <p>פרויקט גמר, תשע"ז</p>
                        <a href="https://www.hit.ac.il/telem/overview">הפקולטה לטכנולוגיות למידה</a>

                        <br />

                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="HITImage" class="aboutImages" runat="server" ImageUrl="~/images/hitlogo.JPG" />
                                </td>
                                <td class="txtTD">
                                    <h5>צוות הפרויקט:</h5>
                                    <p>שירה פיקר והילה פיטם</p>
                                    <h5>מנחות:</h5>
                                    <p>ד"ר היילי וייגלט-מרום, ד"ר דורותי לנגלי</p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="clientImage" class="aboutImages" runat="server" ImageUrl="~/images/logorambam2.jpg" />
                                </td>
                                <td class="txtTD">
                                    <p>בשיתוף עם בית חולים רמב"ם, חיפה</p>
                                    <p>גלית קובי, נציגת הארגון</p>
                                </td>
                            </tr>
                        </table>

                    </div>

                </div>

                <p id="cc">© כל הזכויות שמורות למכון טכנולוגי חולון HIT</p>

            </div>


        </div>

        <!----------תפריט הניווט ----------->
        <nav id="bottomNav">
            <ul>
                <li>
                    <asp:LinkButton ID="profileLinkButton" runat="server" class="navBtn" containernum="0">
                        <asp:Image ID="proflieNavImage" class="navBtnImage" imageNum="0" runat="server" ImageUrl="~/images/profile.svg" />
                        <asp:Label ID="profileLabel" class="navBtnLabel" runat="server" Text="פרופיל"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton runat="server" class="navBtn" containernum="1" disabled="true">
                        <asp:Image ID="howtoplayNavImage" class="navBtnImage" imageNum="1" runat="server" ImageUrl="~/images/gameins.svg" />
                        <asp:Label class="navBtnLabel" runat="server" Text="איך משחקים"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="gameNavBtn" runat="server" class="navBtn" containernum="2" disabled="true">
                        <asp:Image ID="gameNavImage" class="navBtnImage" imageNum="2" runat="server" ImageUrl="~/images/playgame.svg" />
                        <asp:Label class="navBtnLabel" runat="server" Text="משחק"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton runat="server" class="navBtn" containernum="3" OnClick="Results_Click" disabled="true">
                        <asp:Image ID="resultsNavImage" class="navBtnImage" imageNum="3" runat="server" ImageUrl="~/images/participants.svg" />
                        <asp:Label class="navBtnLabel" runat="server" Text="תוצאות"></asp:Label>
                    </asp:LinkButton>
                </li>
                <li>

                    <asp:LinkButton runat="server" class="navBtn" containernum="4" disabled="true">
                        <asp:Image ID="moreNavImage" class="navBtnImage" imageNum="4" runat="server" ImageUrl="~/images/about.svg" />
                        <asp:Label class="navBtnLabel" runat="server" Text="אודות"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </nav>


    </form>


</body>
</html>
