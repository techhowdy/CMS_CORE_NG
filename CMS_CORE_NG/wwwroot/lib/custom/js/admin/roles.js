/* When the View loads - we will get all user roles and role permission (is any) and display them in the tabulator table */

$(function () {
    GetUserRoles();
    GetAllRolePermissionsTypes();

});

/* GLOBAL VARIABLES */
var RoleData = { "roleName": "" };
var RoleType = {};
let ApplicationRole = { "RoleName": "", "IsActive": "", "Handle": "", "Permissions": [] };

/* INITIAIALIZE THE TABULATOR TABLE */
var table = new Tabulator("#userRoleTable", {
    layout: "fitColumns",      //fit columns to width of table
    responsiveLayout: "hide",  //hide columns that dont fit on the table
    tooltips: true,            //show tool tips on cells
    addRowPos: "top",          //when adding a new row, add it to the top of the table
    history: true,             //allow undo and redo actions on the table
    pagination: "local",       //paginate the data
    paginationSize: 7,         //allow 7 rows per page of data
    movableColumns: true,      //allow column order to be changed
    resizableRows: false,       //allow row order to be changed
    initialSort: [             //set the initial sort order of the data
        { column: "id", dir: "asc" }
    ],
    columns: [                //define the table columns
        { title: "Name", field: "name" },
        {
            title: "Handle", field: "handle", formatter: function (cell) {
                return "@" + cell.getData().handle;
            }
        },
        {
            title: "Icon", field: "roleIcon", formatter: function (cell) {
                var roleIcon = cell.getData().roleIcon;
                var imageCell = "<img src='" + roleIcon + "' class='img-thumbnail rounded-circle' style='width: 35px; height: 35px; padding: 0' />";
                return imageCell;
            }
        },
        { title: "Active", field: "isActive", formatter: "tickCross" },
        {
            title: "Actions", align: "center", formatter: function (cell) {
                var roleId = cell.getData().id;
                var roleName = cell.getData().name;
                var roleNormalName = cell.getData().normalizedName;
                var permissions = cell.getData().permissions;
                var icon = cell.getData().roleIcon;
                var handle = cell.getData().handle;
                var active = cell.getData().isActive;

                var newEditRow = "<div class='btn-group' role='group' aria-label='Perform Actions'>" +
                    "<button type='button' name='View'  class='btn btn-success btn-sm' onclick='loadRoleView(this)' " +
                    "data-roleid='" + roleId + "' " +
                    "data-name='" + roleName + "' " +
                    "data-handle='" + handle + "' " +
                    "data-normalname='" + roleNormalName + "' " +
                    "data-active='" + active + "' " +
                    "data-permissions='" + JSON.stringify(permissions) + "' " +
                    "data-icon='" + icon + "' " +
                    ">" +
                    "<span>" +
                    "<i class='fa fa-eye'>" +
                    "</i>" +
                    "</span>" +
                    "</button>" +
                    "<button type='button' name='Edit'  class='btn btn-primary btn-sm' onclick='editRoleById(this)' " +
                    "data-editid='" + roleId + "' " +
                    "data-name='" + roleName + "' " +
                    "data-handle='" + handle + "' " +
                    "data-active='" + active + "' " +
                    "data-normalname='" + roleNormalName + "' " +
                    "data-permissions='" + JSON.stringify(permissions) + "' " +
                    "data-icon='" + icon + "' " +
                    ">" +
                    "<span>" +
                    "<i class='fa fa-edit'>" +
                    "</i>" +
                    "</span>" +
                    "</button>" +
                    "<button type='button' name='Delete' data-delid='" + roleId + "'  class='btn btn-danger btn-sm' onclick='deleteRoleById(this)' " +
                    "data-name='" + roleName + "' " +
                    ">" +
                    "<span>" +
                    "<i class='fa fa-trash'>" +
                    "</i>" +
                    "</span>" +
                    "</button>" +
                    "</div>";

                return newEditRow;
            }
        }
    ]
});

/* METHOD USED TO RESET THE FORM ID AFTER SUBMISSION */
function resetForm(formId) {
    $("#" + formId + "").trigger('reset');
    $("#imgProfile").attr("src", "/uploads/user/profile/default/profile.jpeg");
}

/* METHOD USED TO FETCH EXISTING ROLES FROM DATABASE */
function GetUserRoles() {
    $.ajax({
        type: 'GET',
        url: "/api/v1/UserRole/GetRoles",
        dataType: 'json',        
        success: function (data) {

            var obj = JSON.stringify(data);

            table.setData(obj);

        }
    });
}

/* METHOD USED TO FETCH EXISTING ROLE PERMISSIONS FROM DATABASE */
function GetAllRolePermissionsTypes() {
    $.ajax({
        type: 'GET',
        url: "/api/v1/UserRole/GetAllRolePermissionsTypes",
        dataType: 'json',        
        success: function (data) {
            $.each(data, function (k, v) {
                RoleType[k] = { "id": v.id, "type": v.type };
            });
        },
        error: function (error) {
            Swal.fire({
                type: 'error',
                title: 'Oops...',
                text: 'Request cannot be completed. Please try again later.'
            })
        }
    });
}

/* EXETNSION METHOD USED TO LOAD INITIAL VIEW - ROLES */
function loadRoleView(value) {
    var roleId = $(value).data('roleid');
    var permissions = $(value).data('permissions');

    /* Check for new role type entries added in system */
    $.each(RoleType, function (i, j) {
        let userCurrentRoleTypes = permissions.filter((x) => { return x.type.toLowerCase() === j.type.toLowerCase() });
        if (userCurrentRoleTypes.length == 0) {
            var obj = { "id": j.id, "type": j.type, "add": false, "update": false, "delete": false, "read": false };
            permissions.push(obj);
        }
    });

    $("#permissionTableView tbody").html("");
    $.each(permissions, function (k, v) {
        addOrViewPermissionRow(v, true, "permissionTableView");
    });

    $("#ViewRoleModal .modal-title").html("Role Details");
    $("#roleNameView").val($(value).data('name'));
    $("#roleHandleView").val($(value).data('handle'));
    $("#roleActiveView").prop('checked', $(value).data('active'));
    $("#roleIdView").text(roleId);
    $("#imgProfileView").attr("src", $(value).data('icon'));
    $("#ViewRoleModal").modal('show');
}

/* EXETNSION METHOD USED TO LOAD MODAL */
function loadPermissionTypeModal() {

    $("#PermissionTypeModal .modal-title").html("Add Permission Type");
    $("#PermissionTypeModal").modal('show');

}

/* EXETNSION METHOD USED TO ADD ALL CREATED ROWS TO TABLE BODY */
function addOrViewPermissionRow(value, isViewOnly, tableId) {
    // First check if the <tbody> tag already exists, if not add one             
    if ($("#" + tableId + " tbody").length == 0) {
        $("#" + tableId + "").append("<tbody></tbody>");
    }
    // Now Lets append row to the table
    $("#" + tableId + " tbody").append(BuildPermissionTableRow(value, isViewOnly)); // We will use extension method to do that.


}

/* EXETNSION METHOD USED TO CREATE ROWS TO ADD TO TABLE BODY */
function BuildPermissionTableRow(value, isViewOnly) {
    const valueType = value.type.charAt(0).toUpperCase() + value.type.slice(1);

    if (isViewOnly) {
        let newRow = "<tr>";
        newRow += "<td style='width: 180px !important' data-id='" + value.id + "' class='permissionType'>" + valueType + "</td>";
        if (value.add) {
            newRow += "<td class='text-center'><input disabled type='checkbox' checked data-id='" + value.id + "' data-type='" + valueType + "' name='Add' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input disabled type='checkbox' data-id='" + value.id + "'  data-type='" + valueType + "' name='Add' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        if (value.update) {
            newRow += "<td class='text-center'><input disabled type='checkbox' data-id='" + value.id + "' checked data-type='" + valueType + "' name='Update' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input disabled type='checkbox' data-id='" + value.id + "' data-type='" + valueType + "' name='Update' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        if (value.delete) {
            newRow += "<td class='text-center'><input disabled type='checkbox' checked  data-id='" + value.id + "' data-type='" + valueType + "' name='Delete' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input disabled type='checkbox' data-id='" + value.id + "'  data-type='" + valueType + "' name='Delete' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        if (value.read) {
            newRow += "<td class='text-center'><input disabled type='checkbox' checked data-id='" + value.id + "'  data-type='" + valueType + "' name='Read' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input disabled type='checkbox' data-id='" + value.id + "'  data-type='" + valueType + "' name='Read' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        newRow += "</tr>";
        return newRow;
    }
    else {
        let newRow = "<tr>";
        newRow += "<td style='width: 180px !important' data-id='" + value.id + "' class='permissionType'>" + valueType + "</td>";
        if (value.add) {
            newRow += "<td class='text-center'><input type='checkbox' checked data-id='" + value.id + "' data-type='" + valueType + "' name='Add' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input type='checkbox' data-id='" + value.id + "'  data-type='" + valueType + "' name='Add' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        if (value.update) {
            newRow += "<td class='text-center'><input type='checkbox' checked data-id='" + value.id + "' data-type='" + valueType + "' name='Update' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input type='checkbox' data-id='" + value.id + "' data-type='" + valueType + "' name='Update' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        if (value.delete) {
            newRow += "<td class='text-center'><input type='checkbox' checked data-id='" + value.id + "'  data-type='" + valueType + "' name='Delete' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input type='checkbox' data-id='" + value.id + "'  data-type='" + valueType + "' name='Delete' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        if (value.read) {
            newRow += "<td class='text-center'><input type='checkbox' checked data-id='" + value.id + "'  data-type='" + valueType + "' name='Read' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        else {
            newRow += "<td class='text-center'><input type='checkbox' data-id='" + value.id + "'  data-type='" + valueType + "' name='Read' class='form-check-input' style='position: inherit; margin: 0'></td>";
        }
        newRow += "</tr>";
        return newRow;
    }
}

/* METHOD USED TO CONFIRM DELETION OF EXISTING ROLE FROM DATABASE */
function deleteRoleById(value) {
    var delId = $(value).data('delid');
    var delRoleName = $(value).data('name');
    $("#DeleteRoleModal").modal('show');
    $("#DeleteRoleModal .modal-title").html("Delete Confirmation");
    $("#DeleteRoleModal .modal-body").html("Do You Want To Delete Role : " + "<strong class='text-danger'><span>" + delRoleName + " with Id</span><span id='toDeleteRL'>" + delId + "</span></strong>");
}

/* EXTENSION METHOD USED TO ADD ROLE IDS TO EACH ROW - BEFORE LOADING ROLE PERMISSION MODAL  */
function addRoleById() {
    // Get all the role types
    $("#permissionTable tbody").html("");


    $.each(RoleType, function (k, v) {

        addOrViewPermissionRow(v, false, "permissionTable");
    });
    $("#roleError").text("");
    $("#AddRoleModal").modal('show');
    $("#AddRoleModal .modal-title").html("Add New Role");

}

/* EXTENSION METHOD USED TO LOAD EDIT ROLE PERMISSION MODAL */
function editRoleById(value) {
    var permissions = $(value).data('permissions');
    $("#permissionTableEdit tbody").html("");

    /* Check for new role type entries added in system */
    $.each(RoleType, function (i, j) {
        let userCurrentRoleTypes = permissions.filter((x) => { return x.type.toLowerCase() === j.type.toLowerCase() });
        if (userCurrentRoleTypes.length == 0) {
            var obj = { "id": 0, "type": j.type, "add": false, "update": false, "delete": false, "read": false };
            permissions.push(obj);
        }
    });

    $.each(permissions, function (k, v) {
        addOrViewPermissionRow(v, false, "permissionTableEdit");
    });

    $("#EditRoleModal .modal-title").html("Update Role Details");
    $("#roleErrorEdit").text("");
    $("#roleNameEdit").val($(value).data('name'));
    $("#roleHandleEdit").val($(value).data('handle'));
    $("#roleActiveEdit").prop("checked", $(value).data('active'));
    $("#roleIdEdit").text($(value).data('editid'));
    $("#imgProfileEdit").attr("src", $(value).data('icon'));

    $("#EditRoleModal").modal('show');
}

/* METHOD USED TO DELETE EXISTING ROLE FROM DATABASE */
function confirmDeleteRole() {
    var idRL = $("#toDeleteRL").text();
    // handle deletion here
    if (idRL !== "") {
        $.ajax({
            type: "DELETE",
            url: "/api/v1/UserRole/DeleteRole/" + idRL,
            contentType: "application/json",
            headers: {
                'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
            },
            async: true,            
            success: function (result) {
                resetForm("DeleteRoleModalForm");
                $("#DeleteRoleModal").modal('hide');

                Swal.fire(
                    'Sucess!',
                    'Role : ' + idRL + ' deleted.',
                    'success'
                );
                GetUserRoles();
            },
            error: function (error) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Request cannot be completed. Please try again later.'
                })
            }
        });
    }
}

/* METHOD USED TO ADD ROLE TO DATABASE */
function confirmAddRole(event) {
    let postData = new FormData(event.form);
    const PermissionsArray = Object.values(RoleType);

    $('#permissionTable input[type=checkbox]').each(function () {
        let type = $(this).data("type");

        let index = PermissionsArray.findIndex(obj => obj.type === type);
        PermissionsArray[index][this.name] = (this.checked ? 1 : 0);
        delete PermissionsArray[index].id;           
    });
    
    ApplicationRole.RoleName = $("#roleName").val();
    ApplicationRole.IsActive = $("#roleActive").is(":checked");
    ApplicationRole.Handle = $("#roleHandle").val();
    ApplicationRole.Permissions = PermissionsArray;
    postData.append("ApplicationRole", JSON.stringify(ApplicationRole));


    if (ApplicationRole.RoleName !== "") {
        $.ajax({
            type: "POST",
            url: "/api/v1/UserRole/AddRole/",
            data: postData,
            contentType: false,
            processData: false,
            headers: {
                'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
            },
            async: true,            
            success: function (result) {
                resetForm("AddRoleModalForm");
                $("#AddRoleModal").modal('hide');

                Swal.fire(
                    'Sucess!',
                    'Role : ' + RoleData.roleName + ' added.',
                    'success'
                );
                GetUserRoles();
            },
            error: function (error) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Request cannot be completed. Please try again later.'
                })
            }
        });

    }
    else {
        $("#roleError").text("Please provide name for role.");
    }
}

/* METHOD USED TO UPDATE ROLE TO DATABASE */
function confirmUpdateRole(event) {
    let postData = new FormData(event.form);

    const PermissionsArray = Object.values(RoleType);

    $('#permissionTableEdit input[type=checkbox]').each(function () {
        let type = $(this).data("type");
        let permissionId = $(this).data("id");
        let index = PermissionsArray.findIndex(obj => obj.type === type);
        PermissionsArray[index][this.name] = (this.checked ? 1 : 0);
        PermissionsArray[index].id = permissionId;
    });

    ApplicationRole.Id = $("#roleIdEdit").text();
    ApplicationRole.RoleName = $("#roleNameEdit").val();
    ApplicationRole.IsActive = $("#roleActiveEdit").is(":checked");
    ApplicationRole.Handle = $("#roleHandleEdit").val();
    ApplicationRole.Permissions = PermissionsArray;
    postData.append("ApplicationRole", JSON.stringify(ApplicationRole));

    if (ApplicationRole.RoleName !== "") {
        $.ajax({
            type: "PUT",
            url: "/api/v1/UserRole/UpdateRole/",
            data: postData,
            contentType: false,
            processData: false,
            async: true,
            headers: {
                'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
            },
            success: function (result) {
                resetForm("UpdateRoleModalForm");
                $("#EditRoleModal").modal('hide');

                Swal.fire(
                    'Sucess!',
                    'Role : ' + ApplicationRole.RoleName + ' updated.',
                    'success'
                );
                GetUserRoles();
            },
            error: function (error) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Request cannot be completed. Please try again later.'
                })
            }
        });

    }
    else {
        $("#roleErrorEdit").text("Please provide name for role.");
    }

}

/* METHOD USED TO CREATE NEW ROLE PERMISSION TYPE IN DATABASE */
function addRolePermissionType(event) {
    var PermissionTypeName = $("#PermissionTypeName").val();

    if (PermissionTypeName !== "") {
        $.ajax({
            type: "POST",
            url: "/api/v1/UserRole/AddRolePermission/" + PermissionTypeName,
            contentType: "application/json",
            headers: {
                'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
            },
            async: true,
            success: function (result) {
                $("#PermissionTypeName").val("");
                $("#RoleTypeModal").modal('hide');

                Swal.fire(
                    'Sucess!',
                    'Role : ' + PermissionTypeName + ' added.',
                    'success'
                );
                GetAllRolePermissionsTypes();
            },
            error: function (error) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Request cannot be completed. Please try again later.'
                })
            }
        });

    }
    else {
        $("#PermissionTypeNameError").text("Please provide permission type.");
    }
}

/* EXTENSION METHOD TO GET BROWSER COOKIE */
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}