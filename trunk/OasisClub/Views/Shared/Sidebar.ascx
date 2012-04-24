<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<script type="text/javascript">
    // This will dynamically add the selected class to the current li item
    $(document).ready(SetSelectedListItem);

    function SetSelectedListItem() {
        var currentAction = '<%= Url.RequestContext.RouteData.Values["action"] %>';
        var liToChange = "#navli_" + currentAction;
        $(liToChange).parent().addClass("selected");
    }
</script>

<ul>
    <li class="first"><%= Html.ActionLink("Home", "Index", null, new {id="navli_Index"}) %></li>
    <li><%= Html.ActionLink("About Us", "About", null, new { id = "navli_About" })%></li>
    <li><%= Html.ActionLink("Staff", "Staff", null, new { id = "navli_Staff" })%></li>
    <li><%= Html.ActionLink("Construction", "Construction", null, new { id = "navli_Construction" })%></li>
    <li><%= Html.ActionLink("C4 Academy", "C4Academy", null, new { id = "navli_C4Academy" })%></li>
    <li><%= Html.ActionLink("Membership", "Membership", null, new { id = "navli_Membership" })%></li>
    <li><%= Html.ActionLink("Members Area", "Members", null, new { id = "navli_Members" })%></li>
    <li><%= Html.ActionLink("Calendar", "Calendar", null, new { id = "navli_Calendar" })%></li>
    <li><%= Html.ActionLink("FAQ", "FAQ", null, new { id = "navli_FAQ" })%></li>
    <li><%= Html.ActionLink("Contact Us", "Contact", null, new { id = "navli_Contact" })%></li>
    <li class="last"><%= Html.ActionLink("Policy", "Policy", null, new { id = "navli_Policy" })%></li>
</ul>