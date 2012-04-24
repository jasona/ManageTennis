<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Oasis.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Memberships</h2>

    <div id="Membership_Top">
        <div id="Membership_Left">
            <h3>1st 100 Members Special</h3>

            <div>
                <p>OPTION 1:</p>
                <table>
                <thead>
                    <tr>
                        <th>Monthly Dues</th>
                        <th>Months</th>
                        <th>Initiation</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Phase 1 - $75</td>
                        <td>1 - 6</td>
                        <td>$0</td>
                    </tr>
                    <tr>
                        <td>Phase 2 - $100</td>
                        <td>7 - 12</td>
                        <td>$1000</td>
                    </tr>
                    <tr>
                        <td>Phase 3 - $125</td>
                        <td>13+</td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot></tfoot>
            </table>
            </div>
            <div>
                <p>OPTION 2:</p>
                <table>
                <thead>
                    <tr>
                        <th>Monthly Dues</th>
                        <th>Months</th>
                        <th>Initiation</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Phase 1 - $125</td>
                        <td>1 - 6</td>
                        <td>$0</td>
                    </tr>
                    <tr>
                        <td>Phase 2 - $150</td>
                        <td>7 - 12</td>
                        <td>$250</td>
                    </tr>
                    <tr>
                        <td>Phase 3 - $175</td>
                        <td>13+</td>
                        <td></td>
                    </tr>
                </tbody>
                <tfoot></tfoot>
            </table>
            </div>
        </div><!-- END Membership_Left -->
        
        <div id="Membership_Right">
            <table>
                <thead>
                    <tr>
                        <th></th>
                        <th colspan="2">Join in PHASE I*</th>
                        <th colspan="2">Join in PHASE II*</th>
                    </tr>
                    <tr>
                        <th>Membership</th>
                        <th>Initiation</th>
                        <th>Dues</th>
                        <th>Initiation</th>
                        <th>Dues</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Family</td>
                        <td>$250</td>
                        <td>$175</td>
                        <td>$1000</td>
                        <td>$195</td>
                    </tr>
                    <tr>
                        <td>Single</td>
                        <td>$200</td>
                        <td>$125</td>
                        <td>$800</td>
                        <td>$150</td>
                    </tr>
                    <tr>
                        <td>Senior</td>
                        <td>$200</td>
                        <td>$100</td>
                        <td>$800</td>
                        <td>$125</td>
                    </tr>
                </tbody>
                <tfoot></tfoot>
            </table>
        </div><!-- END Membership_Right -->
    </div><!-- END Membership_Top -->

    <div>
        <a href="http://www.oasisbeachandtennisclub.com/MembershipBrochure.pdf" target="_blank">
            >> OASIS Membership Application</a>
    </div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
