
// JavaScript source code
function ConfirmDelete() {
    if (confirm("Are you sure you want to Delete ?") == true)
        return true;
    else
        return false;
}  

function ConfirmApprove() {
    if (confirm("Are you sure you want to Approve holiday ?") == true)
        return true;
    else
        return false;
}  

//function ConfirmReject() {

//    if (confirm("Are you sure you want to Reject holiday ?") == true) {
//        var reason = prompt("Reason for Rejection:")
//        if (reason) {
//            sessionStorage.setItem("reason", reason);
//            document.getElementById('<%=hdAdminRemark.ClientID%>').value = sessionStorage.getItem("reason");
//            return true;
//        }
//        else {
//            return false;
//        }
//    }
//    else {
//        return false;
//    }
//}  