// Visar delete-sidan i en modal
var DeleteUser = function (id) {
    $.ajax({

        type: "GET",
        url: "/Admin/DeleteUser",
        data: { Id: id },
        success: function (response) {
            $(".modal-body").html(response);
            $("#deleteDialog").modal("show");
        }
    });
};