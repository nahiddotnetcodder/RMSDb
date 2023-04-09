var rowIdx = 0;
var rowTableIdx = 0;
var rowJournalTableIdx = 0;
var allAccounts = [];
var itemList = [];
var rsditemCode = '';
var ddlitemName = 0;
var rsduPrice = 0;
var rsdQty = 0;
var rsdTotalPrice = 0;
var rsdKotNo = 0;

$(".onlyNumber").change(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});
$("#rsmPerson").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});


function dateValue() {
    $.ajax({
        type: "GET",
        url: "/ResSales/DateValue",
        success: function (data) {
            if (data != null) {
                var data = new Date(data);
                var day = ("0" + data.getDate()).slice(-2);
                var month = ("0" + (data.getMonth() + 1)).slice(-2);
                var dayClose = data.getFullYear() + "-" + (month) + "-" + (day);
                $("#rdcDate").val(dayClose);

            }
        }
    });
}
function getInitData() {
    $.ajax({
        type: "GET",
        url: "/ResSales/GetInitData",
        success: function (response) {
            if (response != null) {
                var empNameDD = response.empNameDD;
                var accounts = response.chartMasterDD;
                allAccounts = response.chartMasterDD;
                var subGroups = response.chartTypeDD;
                $('#ddlitemName').empty();
                $('#ddlitemName').append(new Option("--Select Item Name--", -1))

                $('#ddlempName').append(new Option("--Select Waiter Name--", -1))
                for (var i = 0; i < empNameDD.length; i++) {
                    var option = new Option(empNameDD[i].name, empNameDD[i].id);
                    $(option).html(empNameDD[i].name);
                    $("#ddlempName").append(option);
                }

                for (var i = 0; i < subGroups.length; i++) {
                    var optgroup = "<optgroup label='" + subGroups[i].name + "'>";
                    var data = accounts.filter(x => x.accountGroupId == subGroups[i].id);
                    for (var j = 0; j < data.length; j++) {
                        optgroup += "<option value='" + data[j].id + "'>" + data[j].name + "</option>"
                    }
                    optgroup += "</optgroup>";
                    $("#ddlitemName").append(optgroup);
                }
            }
        }
    });
}
function getDescriptionDD() {
    $.ajax({
        type: "GET",
        url: "/StoreGReceive/GetInitData",
        success: function (response) {
            if (response != null) {
                var empDetails = response.empDetails;
                var accounts = response.chartMasterDD;
                allAccounts = response.chartMasterDD;
                var subGroups = response.chartTypeDD;
                $('#ddlitemName').empty();
                $('#ddlitemName').append(new Option("--Select Item Name--", -1))

                $('#ddlsupplierName').append(new Option("--Select Suppliers Name--", -1))
                for (var i = 0; i < empDetails.length; i++) {
                    var option = new Option(empDetails[i].name, empDetails[i].id);
                    $(option).html(empDetails[i].name);
                    $("#ddlsupplierName").append(option);
                }

                for (var i = 0; i < subGroups.length; i++) {
                    var optgroup = "<optgroup label='" + subGroups[i].name + "'>";
                    var data = accounts.filter(x => x.accountGroupId == subGroups[i].id);
                    for (var j = 0; j < data.length; j++) {
                        optgroup += "<option value='" + data[j].id + "'>" + data[j].name + "</option>"
                    }
                    optgroup += "</optgroup>";
                    $("#ddlitemName").append(optgroup);
                }
            }
        }
    });
}
function addItem() {
    var result = validationCheckForItem();
    if (result == false) { return; }
    var descriptionId = $("#ddlitemName option:selected").val();
    var selectedAccount = allAccounts.filter(x => x.id == Number(descriptionId))[0];
    
    let objectItem = {
        itemCode: $("#rsditemCode").val(),
        itemDescriptionId: descriptionId,
        itemDescriptionText: selectedAccount.description,
        rsduPrice: $("#rsduPrice").val(),
        rsdQty: $("#rsdQty").val(),
        rsdTotalPrice: $("#rsdTotalPrice").val(),
        rsdKotNo: $("#rsdKotNo").val(),
    };

    $('#tGLPostingListbody').append(
        `<tr id="Item${++rowTableIdx}">
            <td hidden id="rsdId${rowTableIdx}">` + 0 + `</td>
            <td id="rsditemCode${rowTableIdx}">` + objectItem.rsditemCode + `</td>
            <td hidden id="itemDescriptionId${rowTableIdx}">` + objectItem.itemDescriptionId + `</td>
            <td id="rsdItemName${rowTableIdx}">` + objectItem.itemDescriptionText + `</td>
            <td id="rsduPrice${rowTableIdx}">` + objectItem.rsduPrice + `</td>
            <td id="rsdQty${rowTableIdx}">` + objectItem.rsdQty + `</td>
            <td id="rsdTotalPrice${rowTableIdx}">` + objectItem.rsdTotalPrice + `</td>
            <td id="rsdKotNo${rowTableIdx}">` + objectItem.rsdKotNo + `</td>
            <td class="text-center">
                <button class="btn btn-sm btn-danger remove" type="button"><i class="fa fa-trash" aria-hidden="true"></i></button>
            </td>
        </tr>`
    );
    resetFrom();
}
function validationCheckForItem() {

    var response = true;

    var itemCode = $("#itemCode").val();
    var ddlitemName = parseInt($("#ddlitemName option:selected").val());
    var grdQty = $("#grdQty").val();
    var grduPrice = $('#grduPrice').val();

    showErrorMessageBelowCtrl('itemCode', 'Item Code is required', false);
    showErrorMessageBelowCtrl('ddlitemName', 'Item Name is required', false);
    showErrorMessageBelowCtrl('grdQty', 'Quantity is required', false);
    showErrorMessageBelowCtrl('grduPrice', 'Unit Price is required', false);

    if (grduPrice == undefined || grduPrice.length <= 0) {
        showErrorMessageBelowCtrl('grduPrice', 'Unit is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#grduPrice").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('grduPrice', 'Unit is required', false);
    }

    if (itemCode == undefined || itemCode.length <= 0) {
        showErrorMessageBelowCtrl('itemCode', 'Item Code  is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#itemCode").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('itemCode', 'Item Code  is required', false);
    }
    if (isNaN(ddlitemName) || ddlitemName <= 0) {
        showErrorMessageBelowCtrl('ddlitemName', 'Item Name is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#ddlitemName").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('ddlitemName', 'Item Name is required', false);
    }
    if (grdQty == undefined || grdQty.length <= 0) {
        showErrorMessageBelowCtrl('grdQty', 'Quantity is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#grdQty").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('grdQty', 'Quantity is required', false);
    }

    return response;
}
function resetFrom() {
    $('#rsditemCode').val("");
    $("#ddlitemName").val(-1).change();
    $('#rsduPrice').val("");
    $('#rsdQty').val("");
    $('#rsdTotalPrice').val("");
    $('#rsdKotNo').val("");
}

function SaveRequest() {
    var result = validationCheck();
    if (result == false) { return; }
    var data = new FormData(); 

    data.append('RSMId', 0);
    data.append('RDCDate', $('#rdcDate').val());
    data.append('HREDId', $('#ddlempName').val());
    data.append('RSMTableNo', $('#rsmTableNo').val());
    data.append('RSMPerson', $('#rsmPerson').val());
    
    var tablelength = $('#tSalesPostingListbody tr').length;
    for (var i = 0; i < tablelength - 1; i++) {
        itemList.push({
            rsdId: Number($('#rsdId' + i).val()) ?? 0,
            rsditemCode: $('#rsditemCode' + i).val(),
            itemDescriptionId: $('#ddlitemName' + i).find(":selected").val(),
            rsditemName: $('#ddlitemName' + i).find(":selected").text(),
            rsduPrice: $('#rsduPrice' + i).val(),
            rsdQty: $('#rsdQty' + i).val() ?? 0,
            rsdTotalPrice: $('#rsdTotalPrice' + i).val() ?? 0,
            rsdKotNo: $('#rsdKotNo' + i).val() ?? 0,
            
        });
    }
    data.append('RSItems', JSON.stringify(itemList));

    $.ajax({
        processData: false,
        contentType: false,
        type: "POST",
        url: "/ResSales/Save",
        data: data,
        enctype: 'multipart/form-data',
        beforeSend: function () {
        },
        success: function (response) {
            if (response.success) {
                alertify.notify('Saved Successfully!', 'success', 1, function () { window.location = "/ResSales/masterDetails"; });
            }
            else {
                alertify.error(response.message);
            }
        },
        complete: function () {
            console.log("complete");
        },
        failure: function (response) {
            alertify.error('Something went wrong! please try again.');
        },
        error: function (response) {
            alertify.error('Something went wrong! please try again.');
        }
    });
}
function validationCheck() {

    var response = true;

    var ddlempName = parseInt($("#ddlempName option:selected").val());
    var rsmTableNo = $("#rsmTableNo").val();
    var rsmPerson = $("#rsmPerson").val();
    showErrorMessageBelowCtrl('ddlempName', 'Waiter Name is required', false);
    showErrorMessageBelowCtrl('rsmTableNo', 'Table No. is required', false);
    showErrorMessageBelowCtrl('rsmPerson', 'Person is required', false);

    if (rsmPerson == undefined || rsmPerson.length <= 0) {
        showErrorMessageBelowCtrl('rsmPerson', 'Person is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#rsmPerson").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('rsmPerson', 'Person is required', false);
    }
    if (rsmTableNo == undefined || rsmTableNo.length <= 0) {
        showErrorMessageBelowCtrl('rsmTableNo', 'Table No. is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#rsmTableNo").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('rsmTableNo', 'Table No. is required', false);
    }

    if (isNaN(ddlempName) || ddlempName <= 0) {
        showErrorMessageBelowCtrl('ddlempName', 'Waiter Name is required', true); response = false;
        $('html, body').animate({
            scrollTop: $("#ddlempName").offset().top
        }, 800);
    }
    else {
        showErrorMessageBelowCtrl('ddlempName', 'Waiter Name is required', false);
    }

    return response;
}
$("#addNewButton").click(function () {
    SaveRequest();
});
function getAllReceive() {
    rowJournalTableIdx = 0;
    $.ajax({
        type: "GET",
        url: "/ResSales/GetAll",
        data: "{}",
        success: function (data) {
            let items = data;
            $('#tSaleListbody').empty();
            for (var i = 0; i < items.length; i++) {

                $('#tSaleListbody').append(
                    `<tr id="Item${++rowJournalTableIdx}">
                        <td hidden id="rsmId${rowJournalTableIdx}">` + items[i].rsmId + `</td>
                        <td id="rdcDateString${rowJournalTableIdx}">` + items[i].rdcDateString + `</td>
                        <td id="hrEmpDetailsName${rowJournalTableIdx}">` + items[i].hrEmpDetailsName + `</td>
                        <td id="rsmTableNo${rowJournalTableIdx}">` + items[i].rsmTableNo + `</td>
                        <td id="rsmPerson${rowJournalTableIdx}">` + items[i].rsmPerson + `</td>
                        <td class="text-center">
                            <button id="` + items[i].rsmId + `" class="btn btn-sm btn-primary view" type="button">Details</button>
                            <a class= "btn btn-sm btn-secondary" href='/ResSales/Edit?id=` + items[i].rsmId + `'>Edit</a>
                            
                        </td>
                    </tr>`
                );
            }
        }
    });
}

$('#tSaleListbody').on('click', '.view', function () {
    window.location = '/ResSales/ItemDetails?id=' + $(this)[0].id;
});
$('#tJournalListbody').on('click', '.edit', function () {
    window.location = '/ResSalaes/Edit?id=' + $(this)[0].id;
});
$('.print').click(function () {
    $("#nonPrintArea").hide();
    $("#viewJournalModal").show();
    window.print();
});
function getForEdit() {
    var url = window.location.href;
    var id = url.substring(url.lastIndexOf('=') + 1);

    $.ajax({
        type: "GET",
        url: "/ResSales/GetById?id=" + id,
        data: "{}",
        success: function (data) {
            allAccounts = data.allAccounts;
            let items = data.items;
            rowIdx = 0;
            $('#tSalesPostingListbody').empty();
            for (var i = 0; i < items.length; i++) {
                $('#tSalesPostingListbody').append(`
                <tr id="R${rowIdx}">
                    <td hidden>
                        <input type="text" class="form-control" id="rsdId${rowIdx}" />
                    </td>
                    <td>
                        <input type="text" class="form-control codeChangesFromRow" id="rsditemCode${rowIdx}" />
                    </td>
                    <td style="position:relative" >
                        <select class="form-control accountChangesFromRow autoSuggestionSelect" id="ddlitemName${rowIdx}"></select>
                        <i style="position:absolute; left:90%; bottom: 5%;" class="fa-solid fa-magnifying-glass text-success search" aria-hidden="true" title="Search"></i>
                    </td>
                    <td>
                        <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsduPrice${rowIdx}" readonly/>
                    </td>
                    <td>
                        <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsdQty${rowIdx}" />
                    </td>
                     <td>
                        <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsdTotalPrice${rowIdx}" readonly/>
                    </td>
                    <td>
                        <input type="text" class="form-control codeChangesFromRow onlyNumber" id="rsdKotNo${rowIdx}" />
                    </td>     
       
                    <td class="text-center">
                        <div id="addItem${rowIdx}" class="row">
                            <a style="cursor:pointer" class="btn btn-primary add"><i class="bi bi-plus-circle"></i>add</a>
                        </div>
                        <div id="updateItem${rowIdx}" class="row" style="flex-wrap:nowrap">
                            <button style='margin-left:2px' class="btn btn-sm  edit" type="button"><i class="fa-solid fa-pencil text-success" aria-hidden="true" title="Edit"></i></button>
                            <button style='margin-left:2px' class="btn btn-sm  remove" type="button"><i class="fa-solid fa-xmark text-danger" aria-hidden="true" title="Delete"></i></button>
                        </div>
                        <div id="saveAsUpdatedItem${rowIdx}" class="row" style="flex-wrap:nowrap">
                            <button style='margin-left:2px' class="btn btn-sm  sav" type="button"><i class="fa-solid fa-check text-success" aria-hidden="true" title="save"></i></button>
                            <button style='margin-left:2px' class="btn btn-sm  can" type="button"><i class="fa-solid fa-xmark text-success" aria-hidden="true" title="cancel"></i></button>
                        </div>
                    </td>
                </tr>`); 
                getAllItemFromDB(rowIdx);
                $("#grdId$" + rowIdx).val(items[i].grdId$);
                $("#itemCode" + rowIdx).val(items[i].itemCode);
                $("#ddlitemName" + rowIdx).val(items[i].itemDescriptionId);
                $("#ddlitemName" + rowIdx).change();
                $("#grdUnit" + rowIdx).val(items[i].grdUnit);
                $("#grdQty" + rowIdx).val(items[i].grdQty);
                $("#grduPrice" + rowIdx).val(items[i].grduPrice);
                $("#grdtPrice" + rowIdx).val(items[i].grdtPrice);

                $("#itemCode" + rowIdx).prop('disabled', true);
                $("#ddlitemName" + rowIdx).prop('disabled', true);
                $("#grdUnit" + rowIdx).prop('disabled', true);
                $("#grdQty" + rowIdx).prop('disabled', true);
                $("#grduPrice" + rowIdx).prop('disabled', true);
                $("#grdtPrice" + rowIdx).prop('disabled', true);

                $("#addItem" + rowIdx).hide();
                $("#updateItem" + rowIdx).show();
                $("#saveAsUpdatedItem" + rowIdx).hide();

                rowIdx += 1;
            }
            addFirstRow(rowIdx);
        }
    });
}


$('#updateButton').click(function () {

    data.append('GRMId', 0);
    data.append('GRMDate', $('#grmDate').val());
    data.append('SSId', $('#ddlsupplierName').val());
    data.append('GRMRemarks', $('#grmRemarks').val());

    var tablelength = $('#tGLPostingListbody tr').length;
    for (var i = 1; i <= tablelength; i++) {
        itemList.push({
            grdId: $('#grdId' + i).text(),
            itemCode: $('#itemCode' + i).text(),
            ddlitemName: $('#itemName' + i).text(),
            grdUnit: $('#grdUnit' + i).text(),
            grdQty: $('#grdQty' + i).text(),
            grduPrice: $('#grduPrice' + i).text(),
            grdtPrice: $('#grdtPrice' + i).text()
        });
    }
    data.append('Items', JSON.stringify(itemList));

    $.ajax({
        processData: false,
        contentType: false,
        type: "POST",
        url: "/StoreGReceive/Update",
        data: data,
        enctype: 'multipart/form-data',
        beforeSend: function () {
        },
        success: function (response) {
            if (response.success) {
                alertify.notify('Update Successfully!', 'success', 1, function () { window.location = "/StoreGReceive/masterDetails"; });
            }
            else {
                alertify.error(response.message);
            }
        },
        complete: function () {
            console.log("complete");
        },
        failure: function (response) {
            alertify.error('Something went wrong! please try again.');
        },
        error: function (response) {
            alertify.error('Something went wrong! please try again.');
        }
    });
});


/*inline edit*/
function getAllItemFromDB(parameter) {
    $.ajax({
        type: "GET",
        url: "/ResSales/GetAllSalesItems",
        data: "{}",
        success: function (response) {
            
            var accounts = response.chartMasterDD;
            var subGroups = response.chartTypeDD;

            var o = new Option("Select Item", "-1");
            $(o).html("Please Select Item");
            $("#ddlitemName" + parameter).append(o);

            for (var i = 0; i < subGroups.length; i++) {
                var optgroup = "<optgroup label='" + subGroups[i].name + "'>";
                var data = accounts.filter(x => x.accountGroupId == subGroups[i].id);
                for (var j = 0; j < data.length; j++) {
                    optgroup += "<option value='" + data[j].id + "'>" + data[j].name + "</option>"
                }
                optgroup += "</optgroup>";
                $("#ddlitemName" + parameter).append(optgroup);
            }
        }
    });
}
$('#tSalesPostingListbody').on('change',
    '.accountChangesFromRow',
    function () {
        let getDomId = $(this).attr('id'); 
        var rowTrackId = getDomId.slice(11);
        var value = $("#ddlitemName" + rowTrackId).val();
        if (value != null) {
            var account = allAccounts.filter(x => x.id == value)[0];

            if (account != undefined) {
                $('#rsditemCode' + rowTrackId).val(account.code);
                $('#rsduPrice' + rowTrackId).val(account.price);
            }
            else {
                $('#rsditemCode' + rowTrackId).val();
                $('#rsduPrice' + rowTrackId).val();
            }
        }
    });
$('#tSalesPostingListbody').on('change',
    '.codeChangesFromRow',
    function () {
        let getDomId = $(this).attr('id');
        var rowTrackId = getDomId.slice(8);
        var value = $("#rsditemCode" + rowTrackId).val();
        if (value != null) {
            var account = allAccounts.filter(x => x.code == value)[0];
            if (account != undefined) {
                $('#ddlitemName' + rowTrackId).val(account.id);
                $('#ddlitemName' + rowTrackId).change();
            }
            else {
                $('#ddlitemName' + rowTrackId).val(-1);
                $('#ddlitemName' + rowTrackId).change();
            }
        }
    });
$('#tSalesPostingListbody').keyup(function () {
     var total = 0;
    var x = $("#rsduPrice" + rowIdx).val();
    var y = $("#rsdQty" + rowIdx ).val();

    var total = x * y;
    $("#rsdTotalPrice" + rowIdx).val(total);
})
$('#tSalesPostingListbody').on('click', '.add', function () {
    let getDomId = $(this).closest('tr').attr('id');
    rowIdx = parseInt(getDomId.slice(1));

    var rsdQty = $("#rsdQty" + rowIdx).val();
    var rsditemCode = $("#rsditemCode" + rowIdx).val();
    if (Number(rsditemCode) == '' || Number(rsditemCode) == undefined) {
        alertify.error('Item Code is Required!');
        response = false;
    }
    else if (Number(rsdQty) == '' || Number(rsdQty) == undefined) {
        alertify.error('Quantity is Required!');
        response = false;
    }

    else {
        rowIdx += 1;
        $('#tSalesPostingListbody').append(`
            <tr id="R${rowIdx}">
                <td hidden>
                    <input type="text" class="form-control" id="rsdId${rowIdx}" />
                </td>
                <td>
                    <input type="text" class="form-control codeChangesFromRow" id="rsditemCode${rowIdx}" />
                </td>
                <td style="position:relative" >
                    <select class="form-control accountChangesFromRow autoSuggestionSelect" id="ddlitemName${rowIdx}"></select>
                    <i style="position:absolute; left:95%; bottom: 5%;" class="fa-solid fa-magnifying-glass text-danger search" aria-hidden="true" title="Search"></i>
                </td>
                <td>
                    <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsduPrice${rowIdx}" readonly/>
                </td>
                <td>
                    <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsdQty${rowIdx}" />
                </td>
                 <td>
                    <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsdTotalPrice${rowIdx}" readonly />
                </td>
                <td>
                    <input type="text" class="form-control codeChangesFromRow onlyNumber" id="rsdKotNo${rowIdx}" />
                </td>     
       
                <td class="text-center">
                    <div id="addItem${rowIdx}" class="row">
                        <a style="cursor:pointer" class="btn btn-primary add"><i class="bi bi-plus-circle"></i>add</a>
                    </div>
                    <div id="updateItem${rowIdx}" class="row" style="flex-wrap:nowrap">
                        <button style='margin-left:2px' class="btn btn-sm  edit" type="button"><i class="fa-solid fa-pencil text-success" aria-hidden="true" title="Edit"></i></button>
                        <button style='margin-left:2px' class="btn btn-sm  remove" type="button"><i class="fa-solid fa-xmark text-danger" aria-hidden="true" title="Delete"></i></button>
                    </div>
                    <div id="saveAsUpdatedItem${rowIdx}" class="row" style="flex-wrap:nowrap">
                        <button style='margin-left:2px' class="btn btn-sm  sav" type="button"><i class="fa-solid fa-check text-success" aria-hidden="true" title="save"></i></button>
                        <button style='margin-left:2px' class="btn btn-sm  can" type="button"><i class="fa-solid fa-xmark text-success" aria-hidden="true" title="cancel"></i></button>
                    </div>
                </td>
            </tr>`); 
        getAllItemFromDB(rowIdx);
        // $('.autoSuggestionSelect').css('width', '100%');
        // $(".autoSuggestionSelect").select2({});
        $("#updateItem" + rowIdx).hide();
        $("#saveAsUpdatedItem" + rowIdx).hide();
        var prevRowIdx = rowIdx - 1;
        $("#addItem" + prevRowIdx).hide();
        $("#rsditemCode" + prevRowIdx).prop('disabled', true);
        $("#ddlitemName" + prevRowIdx).prop('disabled', true);
        $("#rsduPrice" + prevRowIdx).prop('disabled', true);
        $("#rsdQty" + prevRowIdx).prop('disabled', true);
        $("#rsdTotalPrice" + prevRowIdx).prop('disabled', true);
        $("#rsdKotNo" + prevRowIdx).prop('disabled', true);
        $("#updateItem" + prevRowIdx).show();
        $("#saveAsUpdatedItem" + prevRowIdx).hide();
    }
});
$('#tSalesPostingListbody').on('click', '.edit', function () {
    let getDomId = $(this).closest('tr').attr('id');
    rowIdx = parseInt(getDomId.slice(1));

    rsditemCode = $("rsditemCode" + rowIdx).val();
    ddlitemName = $("#ddlitemName" + rowIdx).val();
    rsduPrice = $("#rsduPrice" + rowIdx).val();
    rsdQty = $("#rsdQty" + rowIdx).val();
    rsdTotalPrice = $("#rsdTotalPrice" + rowIdx).val();
    rsdKotNo = $("#rsdKotNo" + rowIdx).val();

    $("#rsditemCode" + rowIdx).prop('disabled', false);
    $("#ddlitemName" + rowIdx).prop('disabled', false);
    $("#rsduPrice" + rowIdx).prop('disabled', false);
    $("#rsdQty" + rowIdx).prop('disabled', false);
    $("#rsdTotalPrice" + rowIdx).prop('disabled', false);
    $("#rsdKotNo" + rowIdx).prop('disabled', false);
    
    $("#updateItem" + rowIdx).hide();
    $("#saveAsUpdatedItem" + rowIdx).show();
});
$('#tSalesPostingListbody').on('click', '.remove', function () {
    $(this).closest('tr').remove();
});
$('#tSalesPostingListbody').on('click', '.sav', function () {
    let getDomId = $(this).closest('tr').attr('id');
    rowIdx = parseInt(getDomId.slice(1));

    $("#rsditemCode" + rowIdx).prop('disabled', true);
    $("#ddlitemName" + rowIdx).prop('disabled', true);
    $("#rsduPrice" + rowIdx).prop('disabled', true);
    $("#rsdQty" + rowIdx).prop('disabled', true);
    $("#rsdTotalPrice" + rowIdx).prop('disabled', true);
    $("#rsdKotNo" + rowIdx).prop('disabled', true);
    $("#updateItem" + rowIdx).show();
    $("#saveAsUpdatedItem" + rowIdx).hide();
});
$('#tSalesPostingListbody').on('click', '.can', function () {
    let getDomId = $(this).closest('tr').attr('id');
    rowIdx = parseInt(getDomId.slice(1));

    $("#rsditemCode" + rowIdx).val(rsditemCode);
    $("#ddlitemName" + rowIdx).val(ddlitemName);
    $("#ddlitemName" + rowIdx).change();
    $("#rsduPrice" + rowIdx).val(rsduPrice);
    $("#rsdQty" + rowIdx).val(rsdQty);
    $("#rsdTotalPrice" + rowIdx).val(rsdTotalPrice);
    $("#rsdKotNo" + rowIdx).val(rsdKotNo);

    $("#rsditemCode" + rowIdx).prop('disabled', true);
    $("#ddlitemName" + rowIdx).prop('disabled', true);
    $("#rsduPrice" + rowIdx).prop('disabled', true);
    $("#rsdQty" + rowIdx).prop('disabled', true);
    $("#rsdTotalPrice" + rowIdx).prop('disabled', true);
    $("#rsdKotNo" + rowIdx).prop('disabled', true);
    $("#updateItem" + rowIdx).show();
    $("#saveAsUpdatedItem" + rowIdx).hide();
});
function addFirstRow(id) {
    rowIdx = Number(id);
    $('#tSalesPostingListbody').append(`
    <tr id="R${rowIdx}">
        <td hidden>
            <input type="text" class="form-control" id="rsdId${rowIdx}" />
        </td>
        <td>
            <input type="text" class="form-control codeChangesFromRow" id="rsditemCode${rowIdx}" />
        </td>
        <td style="position:relative" >
            <select class="form-control accountChangesFromRow autoSuggestionSelect" id="ddlitemName${rowIdx}"></select>
            <i style="position:absolute; left:95%; bottom: 5%;" class="fa-solid fa-magnifying-glass text-danger search" aria-hidden="true" title="Search"></i>
        </td>
        <td>
            <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsduPrice${rowIdx}" readonly/>
        </td>
        <td>
            <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsdQty${rowIdx}" />
        </td>
         <td>
            <input type="text" class="form-control numbersOnly" placeholder="0.00" id="rsdTotalPrice${rowIdx}" readonly/>
        </td>
        <td>
            <input type="text" class="form-control codeChangesFromRow onlyNumber" id="rsdKotNo${rowIdx}" />
        </td>     
       
        <td class="text-center">
            <div id="addItem${rowIdx}" class="row">
                <a style="cursor:pointer" class="btn btn-primary add"><i class="bi bi-plus-circle"></i>add</a>
            </div>
            <div id="updateItem${rowIdx}" class="row" style="flex-wrap:nowrap">
                <button style='margin-left:2px' class="btn btn-sm  edit" type="button"><i class="fa-solid fa-pencil text-success" aria-hidden="true" title="Edit"></i></button>
                <button style='margin-left:2px' class="btn btn-sm  remove" type="button"><i class="fa-solid fa-xmark text-danger" aria-hidden="true" title="Delete"></i></button>
            </div>
            <div id="saveAsUpdatedItem${rowIdx}" class="row" style="flex-wrap:nowrap">
                <button style='margin-left:2px' class="btn btn-sm  sav" type="button"><i class="fa-solid fa-check text-success" aria-hidden="true" title="save"></i></button>
                <button style='margin-left:2px' class="btn btn-sm  can" type="button"><i class="fa-solid fa-xmark text-success" aria-hidden="true" title="cancel"></i></button>
            </div>
        </td>
    </tr>`); 
    getAllItemFromDB(rowIdx);
    // $('.autoSuggestionSelect').css('width', '100%');
    //$(".autoSuggestionSelect").select2({});
    $("#updateItem" + rowIdx).hide();
    $("#saveAsUpdatedItem" + rowIdx).hide();
}