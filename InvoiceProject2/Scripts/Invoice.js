$(document).ready(function () {
    setTime();
    $("#Qty").change(function () {
        CalculateTotal();
    });

    $("#Price").change(function () {
        CalculateTotal();
    });

    $("#BtnAddItem").click(function () {
        if (totalValidation("item")) {
            AddItemToList();
            swal("Good job!", "You added item succesfully!", "success");
        }
        else {
            swal("Ops..!", "There is something wrong,Check things with red colors!", "error");
        }
    });

    $("#btnSave").click(function () {
        if (totalValidation("all")) {
            var total = $("#TotalInvoice").val();
            var check = $("input[name='PaymentMethod']:checked").val();
            if (check == "true") {
                if (total > 10000) {
                    swal("Ops..!", "The Invoice can not exceed 10000 when using credit please change to cash or make delete some items!", "error");
                }
                else {
                    SaveInvoice();
                }
            }
            else {
                SaveInvoice();
            }
        }
        else {
            swal("Ops..!", "There is something wrong,Check things with red colors!", "error");
        }
    });


    $("#btnNew").click(function () {
        swal({
            title: "New,Are you sure?",
            text: "All records will be removed to make new Invoice!",
            icon: "warning",
            buttons: [
                'No, cancel it!',
                'Yes, I am sure!'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                swal({
                    title: 'Deleted!',
                    text: 'Records are successfully Removed!',
                    icon: 'success'
                }).then(function () {
                    Reset();
                    ResetInvoice();
                });
            } else {
                swal("Cancelled", "Your record is safe :)", "error");
            }
        })
    });
});

function setTime() {
    var myDate = document.querySelector(myDate);
    var today = new Date();
    InvoiceDate.value = today.toISOString().substr(0, 10);
}
/*----------when you click add item or save records-------*/
function totalValidation(x) {
    if (x == "item") {
        if (!checkItems()) {
            return false;
        }
    }
    else {
        if (!checkInvoice()) {
            return false;
        }
    }
    return true;
}

function checkItems() {
    var checkItems = 0;
    if (!document.getElementById("ItemName").value.match(/^[a-zA-Z' ']+$/)) {
        document.getElementById("ItemName").style.borderColor = "red";
        checkItems++;
    }
    if (checkItems == 0)
        return true;
    return false;
}

function checkInvoice() {
    var check = 0;
    if (!document.getElementById("Customer").value.match(/^[a-zA-Z' ']+$/)) {
        document.getElementById("Customer").style.borderColor = "red";
        check++;
    }
    const rbs = document.querySelectorAll('input[name="PaymentMethod"]');
    var checkSelect = 0;
    for (const rb of rbs) {
        if (rb.checked) {
            checkSelect++;
            break;
        }
    }
    if (checkSelect == 0) {
        document.getElementById("PaymentMethodLabel").style.color = "red";
        document.getElementById("PaymentMethodLabel2").style.color = "red";
    }
    if (check > 0 || checkSelect == 0) {
        return false;
    }
    return true;
}
//-----------check while input---------
function checkNameOnInput() {
    if (document.getElementById("ItemName").value.match(/^[a-zA-Z' ']+$/)) {
        document.getElementById("ItemName").style.borderColor = "black";
    }
    else {
        document.getElementById("ItemName").style.borderColor = "red";
    }
}
function checkCustomerNameOnInput() {
    if (document.getElementById("Customer").value.match(/^[a-zA-Z' ']+$/)) {
        document.getElementById("Customer").style.borderColor = "black";
    }
    else {
        document.getElementById("Customer").style.borderColor = "red";
    }
}
function checkRadioOnInput() {
    document.getElementById("PaymentMethodLabel").style.color = "#0000ff";
    document.getElementById("PaymentMethodLabel2").style.color = "#0000ff";
}
function SaveInvoice() {
    var objInvoiceDto = {};
    var Invoice = {};
    var itemsList = new Array();
    Invoice.InvoiceDate = $("#InvoiceDate").val();
    Invoice.PaymentMethod = $("input[name='PaymentMethod']:checked").val();
    Invoice.Customer = $("#Customer").val();
    Invoice.Description = $("#Description").val();
    objInvoiceDto.Invoice = Invoice;
    $("#itemTable").find("tr:gt(0)").each(function () {
        var item = {};
        item.ItemName = $(this).find("td:eq(1)").text();
        item.Qty = parseInt($(this).find("td:eq(2)").text());
        item.Price = parseInt($(this).find("td:eq(3)").text());
        itemsList.push(item);
    });
    if (itemsList.length > 0 && checkValues()) {
        swal({
            title: "Save Invoice,Are you sure?",
            text: "You will Save the invoice and remove all records to make new invoice!",
            icon: "warning",
            buttons: [
                'No, cancel it!',
                'Yes, I am sure!'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                swal({
                    title: 'Saved!',
                    text: 'You saved the invoice successfully!',
                    icon: 'success'
                }).then(function () {
                    objInvoiceDto.Items = itemsList;
                    console.log("data", objInvoiceDto)
                    $.ajax({
                        url: "/api/Invoice",
                        method: "post",
                        data: objInvoiceDto
                    })
                    Reset();
                    ResetInvoice();
                });
            }
            else {
                swal("Cancelled", "Your records are safe :)", "error");
            }
        })

    }
    else if (itemsList.length == 0) {
        swal("Ops..!", "You should Add items before saving invoice!", "error");
    }
    else {
        swal({
            title: "You have added item details?",
            text: "Do you want to ignore it and save?",
            icon: "warning",
            buttons: [
                'No, cancel it!',
                'Yes, I am sure!'
            ],
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm) {
                swal({
                    title: 'Saved!',
                    text: 'You saved the invoice successfully!',
                    icon: 'success'
                }).then(function () {
                    objInvoiceDto.Items = itemsList;
                    console.log("data", objInvoiceDto)
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        data: "{obj:" + JSON.stringify(objInvoiceDto) + "}",
                        url: "AddInvoice",
                        success: function (data) {

                        },
                        error: function (result) {

                        }
                    })
                    Reset();
                    ResetInvoice();
                });
            }
            else {
                swal("Cancelled", "Your records are safe :)", "error");
            }
        })
    }
}
function checkValues() {
    if (document.getElementById("ItemName").value == '') {
        return true;
    }
    return false;
}
function Reset() {
    $("#Price").val('1');
    $("#Qty").val('1');
    $("#ItemName").val('');
    $("#TotalItemPrice").val('1.00');
}

function ResetInvoice() {
    $("#TotalInvoice").val('0.000');
    $("#Payment").val('');
    $("#Customer").val('');
    $("#Description").val('');
    $("#itemTable").find("tr:gt(0)").each(function () {
        $(this).closest('tr').remove();
    });
}
function AddItemToList() {
    var itemTable = $("#itemTable");
    var price = $("#Price").val();
    var Qty = $("#Qty").val();
    var itemName = $("#ItemName").val();
    var Total = parseFloat($("#TotalItemPrice").val()).toFixed(2);
    var item = " <tr><td>" + itemName + "</td><td>" + Qty + "</td><td>" + price + "</td><td>" + Total + "</td><td><input type='button' value='Delete' class='btn-danger' onclick='DeleteItem(this)' /></td>   <td><input type='button' value='Edit' class='btn btn-danger Edit' onclick='EditItem(this)' /></td> </tr>";
    itemTable.append(item);
    Reset();
    CalculateInvoiceTotal();
}

function CalculateInvoiceTotal() {
    $("#TotalInvoice").val('0.00');
    var totalInvoice = 0.00;
    $("#itemTable").find("tr:gt(0)").each(function () {
        var total = parseFloat($(this).find("td:eq(3)").text());
        totalInvoice += total;
    });
    $("#TotalInvoice").val(parseFloat(totalInvoice).toFixed(2));
}


function EditItem(itemId) {
    swal({
        title: "Edit,Are you sure?",
        text: "It will be removed from the list to edit it!",
        icon: "warning",
        buttons: [
            'No, cancel it!',
            'Yes, I am sure!'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            swal({
                title: 'Edit is Available!',
                text: 'Record are successfully Deleted!',
                icon: 'success'
            }).then(function () {

                $("#ItemName").val($(itemId).closest('tr').find("td:eq(0)").text());
                $("#Qty").val($(itemId).closest('tr').find("td:eq(1)").text());
                $("#Price").val($(itemId).closest('tr').find("td:eq(2)").text());
                CalculateTotal();
                $(itemId).closest('tr').remove();
                CalculateInvoiceTotal();
            });
        } else {
            swal("Cancelled", "Your record is safe :)", "error");
        }
    })

}



function DeleteItem(itemId) {
    swal({
        title: "Delete,Are you sure?",
        text: "You will not be able to recover this record!",
        icon: "warning",
        buttons: [
            'No, cancel it!',
            'Yes, I am sure!'
        ],
        dangerMode: true,
    }).then(function (isConfirm) {
        if (isConfirm) {
            swal({
                title: 'Deleted!',
                text: 'Record are successfully Deleted!',
                icon: 'success'
            }).then(function () {
                $(itemId).closest('tr').remove();
                CalculateInvoiceTotal();
            });
        } else {
            swal("Cancelled", "Your record is safe :)", "error");
        }
    })

}

function CalculateTotal() {
    var price = $("#Price").val();
    var Qty = $("#Qty").val();
    var total = Qty * price;
    $("#TotalItemPrice").val(parseFloat(total).toFixed(2));
}