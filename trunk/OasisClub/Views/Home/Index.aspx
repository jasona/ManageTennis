﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Oasis.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function theRotator() {
            //Set the opacity of all images to 0
            $('div#rotator ul li').css({ opacity: 0.0 });

            //Get the first image and display it (gets set to full opacity)
            $('div#rotator ul li:first').css({ opacity: 1.0 });

            //Call the rotator function to run the slideshow, 6000 = change to next image after 6 seconds
            setInterval('rotate()', 4000);

        }

        function rotate() {
            //Get the first image
            var current = ($('div#rotator ul li.show') ? $('div#rotator ul li.show') : $('div#rotator ul li:first'));

            //Get next image, when it reaches the end, rotate it back to the first image
            var next = ((current.next().length) ? ((current.next().hasClass('show')) ? $('div#rotator ul li:first') : current.next()) : $('div#rotator ul li:first'));

            //Set the fade in effect for the next image, the show class has higher z-index
            next.css({ opacity: 0.0 })
	            .addClass('show')
	            .animate({ opacity: 1.0 }, 1000);

            //Hide the current image
            current.animate({ opacity: 0.0 }, 1000)
	            .removeClass('show');

        };

        $(document).ready(function () {
            //Load the slideshow
            theRotator();
        });

    </script>

    <div id="rotator">
        <ul>
            <li class="show"><img src="/Content/Images/Home_1.jpg" width="976" height="650"  alt="pic1" /></li>
            <li><img src="/Content/Images/Home_2.jpg" alt="pic2" /></li>
            <li><img src="/Content/Images/Home_3.jpg" alt="pic3" /></li>
            <li><img src="/Content/Images/Home_4.jpg" alt="pic4" /></li>
            <li><img src="/Content/Images/Home_5.jpg" alt="pic5" /></li>
            <li><img src="/Content/Images/Home_6.jpg" alt="pic6" /></li>
        </ul>
    </div>

    <div id="Home">
        <h2>Welcome</h2>
        
        <div class="homeLeft">
            <p>Finally, it is time to relax and enjoy the year round benefits of Oasis Beach & Tennis Club. Located on a secluded, tree-studded property in Rockwall County, this brand new country club offers enjoyment for the whole family. <b>Club opening: December 2010</b></p>
            <a href="http://www.oasisbeachandtennisclub.com/MembershipBrochure.pdf" target="_blank">>>OASIS Membership Application</a>
        </div>

        <div class="homeRight">
            <h3>Club Features</h3>
            <b>PHASE 1&nbsp;&nbsp;|&nbsp;&nbsp;December 2010</b>
            <ul>
                <li>6 Climate Controlled Indoor Tennis Courts</li>
                <li>8 Outdoor Tennis Courts</li>
                <li>Beach Volleyball | Sport Court w/ Backboard</li>
                <li>Bar & Grill</li>
                <li>Pro Shop</li>
            </ul>
            <b>PHASE 2&nbsp;&nbsp;|&nbsp;&nbsp;May 2011</b>
            <ul>
                <li>Pool | Club House | Restaurant/Bar</li>
                <li>Center Court</li>
                <li>Fully equipped fitness center</li>
            </ul>
            <b>PHASE 3&nbsp;&nbsp;|&nbsp;&nbsp;Spring 2012</b>
            <ul>
                <li>2 clay courts | 3 synthetic grass courts</li>
            </ul>
        </div>

        <div class="homeCallout ir">
            <a class="homeSpecialCallout" href="http://www.oasisbeachandtennisclub.com/FLYER.pdf" target="_blank">
                <em></em>&nbsp;</a><!-- 1st 100 MEMBER SPECIAL -->
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
