<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Oasis.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Location</h2>
    <div id="Contact">
        <div id="Contact_Top">
            <div id="Contact_Location_Left">
                <div>
                    <p><b>Physical Address:</b></p>
                    <p>5757 State Hwy 205</p>
                    <p>Rockwall, TX 75032 <a href="http://maps.google.com/maps?q=5757+State+Hwy+205+Rockwall,+TX+75032&ie=UTF8&hl=en&hq=&hnear=5757+Texas+205,+Rockwall,+Texas+75032&ll=32.831063,-96.362629&spn=0.038801,0.087376&z=14" target="_blank">>>MAP</a></p>
                </div>
        
                <div>
                    <p>972.772.7768 office</p>
                    <p>214.279.4555 fax</p>
                </div>

                <div>
                    <p>membership@oasistennis.com</p>
                    <p>academy@oasistennis.com</p>
                </div>
            </div>

            <div id="Contact_Location_Right">
                <p><b>Mailing Address:</b></p>
                <p>519 Interstate 30, Suite 146</p>
                <p>Rockwall, Texas 75087</p>
            </div>
        </div><!-- END Contact_Top -->

        <script type="text/javascript">
            function ContactSubmitted() {
                $("#ModalSubmitNotify").fadeIn("slow").delay(5000).fadeOut("slow");
            }

            function AjaxFailure() {
                $("#ModalSubmitNotify").text("There was an error submitting your information.\nPlease contact us via phone.");
            }
        </script>

        <div id="Contact_Bottom">
            <h2>Contact</h2>

            <% using(Ajax.BeginForm("SubmitContact", new AjaxOptions() { OnBegin="ContactSubmitted",
                                                                         OnFailure="AjaxFailure" })) { %>
                <div>
                    <span>Your Name:</span><%= Html.TextBox("Name") %>
                </div>
                <div>
                    <span>Your Email:</span><%= Html.TextBox("Email") %>
                </div>
                <div>
                    <span>Subject:</span><%= Html.TextBox("Subject") %>
                </div>
                <div>
                    <span>Message:</span><%= Html.TextBox("Message") %>
                </div>
                <div>
                    <input type="submit" value="Send" />
                </div>
            <% } %>
            
            <div id="ModalSubmitNotify" class="modal">
                <p>
                    Thank you for contacting us.</p>
                <p>
                    We will respond soon.</p>
            </div><!-- END SubmitNotify -->
        </div><!-- END Contact_Bottom -->
    </div><!-- END Contact -->

    <div id="Contact_Map">
        <iframe width="400" 
                height="500" 
                frameborder="0" 
                scrolling="no" 
                marginheight="0" 
                marginwidth="0" 
                src="http://maps.google.com/maps?q=5757+State+Hwy+205+Rockwall,+TX+75032&amp;ie=UTF8&amp;hl=en&amp;hq=&amp;hnear=5757+Texas+205,+Rockwall,+Texas+75032&amp;ll=32.831063,-96.362629&amp;spn=0.708718,1.454315&amp;z=10&amp;output=embed">
        </iframe>
    </div><!-- END Contact_Map -->
    

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
