
/* PRE-DEFINED VALUES FOR GENDER DROUPDOWN */
const genderValues = ["Male", "Female", "Transgender", "Two-Spirit", "Cisgender", "Non-Binary", "Gender Neutral", "Prefer Not To Say", "Other"];

/* USED TO TARGET ELEMENTS USING ID */
const $addGenderDropdown = $("#gender");
const $_addGenderDropdown = $("#_gender");
const $shippingCountryDropdown = $("#scountry");
const $_shippingCountryDropdown = $("#_scountry");
const $billingCountryDropdown = $("#country");
const $_billingCountryDropdown = $("#_country");
const $shippingStateDropdown = $("#sstate");
const $_shippingStateDropdown = $("#_sstate");
const $billingStateDropdown = $("#state");
const $_billingStateDropdown = $("#_state");

/* INITIALLY SET COUTRIELS TO EMPTY */
let countries = [];

/* INITIALIZE ELEMENTS WHEN DOCUMENT READY */
$(function () {
    LoadUsers();
    getCountries();

    /* INITIALIZING CHOSEN ELEMENTS */
    let genderOptionsValues = '<option value="Select Gender" disabled selected>Select</option>';

    $.each(genderValues, function (index, item) {
        genderOptionsValues += '<option value="' + item + '">' + item + '</option>';
    });

    $addGenderDropdown.append(genderOptionsValues);
    $addGenderDropdown.chosen();

    $_addGenderDropdown.append(genderOptionsValues);
    $_addGenderDropdown.chosen();


    $shippingStateDropdown.chosen();
    $billingStateDropdown.chosen();

    $_shippingStateDropdown.chosen();
    $_billingStateDropdown.chosen();

    $billingCountryDropdown.chosen().change(function (event) {
        console.log($(event.target).val());
        let countryId = $(event.target).val();

        let country = countries.filter(function (x) { return x.id === Number(countryId) });
        if (country.length > 0) {
            let states = [];
            if (country[0].states !== null && country[0].states !== "") {
                states = country[0].states.split("|");
                let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                $.each(states, function (index, item) {
                    optionsValues += '<option value="' + item + '">' + item + '</option>';
                });
                $billingStateDropdown.html("")
                $billingStateDropdown.append(optionsValues);
                $billingStateDropdown.prop('disabled', false).trigger("chosen:updated");

            }
            else {
                /* Disable DropDown as no states available */
                $billingStateDropdown.val(null);
                $billingStateDropdown.prop('disabled', true).trigger("chosen:updated");
            }
        }
    });
    $shippingCountryDropdown.chosen().change(function (event) {
        console.log($(event.target).val());
        let countryId = $(event.target).val();

        let country = countries.filter(function (x) { return x.id === Number(countryId) });
        if (country.length > 0) {
            let states = [];
            if (country[0].states !== null && country[0].states !== "") {
                states = country[0].states.split("|");
                let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                $.each(states, function (index, item) {
                    optionsValues += '<option value="' + item + '">' + item + '</option>';
                });
                $shippingStateDropdown.html("")
                $shippingStateDropdown.append(optionsValues);
                $shippingStateDropdown.prop('disabled', false).trigger("chosen:updated");

            }
            else {
                /* Disable DropDown as no states available */
                $shippingStateDropdown.val(null);
                $shippingStateDropdown.prop('disabled', true).trigger("chosen:updated");
            }
        }
    });

    $_billingCountryDropdown.chosen().change(function (event) {
        console.log($(event.target).val());
        let countryId = $(event.target).val();

        let country = countries.filter(function (x) { return x.id === Number(countryId) });
        if (country.length > 0) {
            let states = [];
            if (country[0].states !== null && country[0].states !== "") {
                states = country[0].states.split("|");
                let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                $.each(states, function (index, item) {
                    optionsValues += '<option value="' + item + '">' + item + '</option>';
                });
                $_billingStateDropdown.html("")
                $_billingStateDropdown.append(optionsValues);
                $_billingStateDropdown.prop('disabled', false).trigger("chosen:updated");

            }
            else {
                /* Disable DropDown as no states available */
                $_billingStateDropdown.val(null);
                $_billingStateDropdown.prop('disabled', true).trigger("chosen:updated");
            }
        }
    });
    $_shippingCountryDropdown.chosen().change(function (event) {
        console.log($(event.target).val());
        let countryId = $(event.target).val();

        let country = countries.filter(function (x) { return x.id === Number(countryId) });
        if (country.length > 0) {
            let states = [];
            if (country[0].states !== null && country[0].states !== "") {
                states = country[0].states.split("|");
                let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                $.each(states, function (index, item) {
                    optionsValues += '<option value="' + item + '">' + item + '</option>';
                });
                $_shippingStateDropdown.html("")
                $_shippingStateDropdown.append(optionsValues);
                $_shippingStateDropdown.prop('disabled', false).trigger("chosen:updated");

            }
            else {
                /* Disable DropDown as no states available */
                $_shippingStateDropdown.val(null);
                $_shippingStateDropdown.prop('disabled', true).trigger("chosen:updated");
            }
        }
    });
     

    /* EVENT LISTNER FOR ADD USER BUTTON CLICKED - LOCATED IN ADDUSER MODAL LAYOUT */
    $("#addUserFormsubmit").click(function (event) {
        event.preventDefault();

        // Here we will check if our form is valid using jquery unobtrusive library
        if ($('#addUserForm').valid()) {            
            let postData = new FormData(event.target.form);

            if (postData.get("BillingAddress.Country") !== null || postData.get("BillingAddress.Country") !== "") {
                let country = countries.filter(function (x) { return x.id === Number(postData.get("BillingAddress.Country")) });
                postData.set("BillingAddress.Country", country[0].name)
            }
            if (postData.get("ShippingAddress.Country") !== null || postData.get("ShippingAddress.Country") !== "") {
                let country = countries.filter(function (x) { return x.id === Number(postData.get("ShippingAddress.Country")) });
                postData.set("ShippingAddress.Country", country[0].name)
            }

            addUser(postData);
        }
        else {
            var $errors = $("form").find(".field-validation-error span");
            console.log($errors);
        }

    });

    /* EVENT LISTNER FOR EDIT USER BUTTON CLICKED - LOCATED IN ADDUSER MODAL LAYOUT */
    $("#editUserFormSubmit").click(function (event) {
        event.preventDefault();

        if ($('#editUserForm').valid()) {
            var postData = new FormData(event.target.form);

            if (postData.get("BillingAddress.Country") !== null || postData.get("BillingAddress.Country") !== "") {
                let country = countries.filter(function (x) { return x.id === Number(postData.get("BillingAddress.Country")) });
                postData.set("BillingAddress.Country", country[0].name)
            }
            if (postData.get("ShippingAddress.Country") !== null || postData.get("ShippingAddress.Country") !== "") {
                let country = countries.filter(function (x) { return x.id === Number(postData.get("ShippingAddress.Country")) });
                postData.set("ShippingAddress.Country", country[0].name)
            }
            EditUser(postData);
        }
        else {
            var $errors = $("form").find(".field-validation-error span");
            console.log($errors);
        }
    });

    /* INITIALIZE THE DATE TIME PICKER */
    $("#birthdate").datepicker();
    $("#_birthdate").datepicker();
});

/* LOAD USERS - */
function LoadUsers() {   

    $.ajax({
        type: 'GET',
        url: "/api/v1/User/GetUsers",
        dataType: 'json',        
        success: function (data) {

            var obj = JSON.stringify(data);

            /* INITIALIZE THE TABULATOR TABLE AND PADD USER LIST DATA */
            table.setData(obj);
        }
    });
}

/* LOAD COUNTRIES  - */
function getCountries() {
    $.ajax({
        type: "GET",
        url: "/api/v1/Country/GetCountries",
        dataType: "json",
        async: false,        
        success: function (result) {
            countries = result;
            if (countries) {
                let optionsValues = '<option value="Select Country" disabled selected>Select</option>';
                $.each(countries, function (index, item) {
                    optionsValues += '<option value="' + item.id + '">' + item.flag + ' ' + item.name + '</option>';
                });

                $billingCountryDropdown.append(optionsValues);
                $_billingCountryDropdown.append(optionsValues);
                $shippingCountryDropdown.append(optionsValues);
                $_shippingCountryDropdown.append(optionsValues);

                $billingCountryDropdown.chosen();
                $_billingCountryDropdown.chosen();
                $shippingCountryDropdown.chosen();
                $_shippingCountryDropdown.chosen();
            }
        }
    });
}

/* RESET USER PASSWORD - */
function resetPassword() {
    $.ajax({
        type: 'POST',
        url: "/api/v1/user/ResetPassword/" + $("#_username").val(),
        headers: {
            'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
        },
        contentType: false,
        processData: false,        
        success: function (result) {
            $('#addUserForm')[0].reset();
            $("#AddUserModal").modal('hide');
            Swal.fire(
                'Password Reset',
                result,
                'success'
            ).then(LoadUsers())
        }
    });
}

/* ADD USER METHOD - */
function addUser(postData) {    
    (postData.get("IsEmployee")) === "true" ? postData.set("IsEmployee", "1") : postData.set("IsEmployee", "0");
    (postData.get("IsAccountLocked")) === "true" ? postData.set("IsAccountLocked", "1") : postData.set("IsAccountLocked", "0");
    (postData.get("IsPhoneVerified")) === "true" ? postData.set("IsPhoneVerified", "1") : postData.set("IsPhoneVerified", "0");
    (postData.get("IsTwoFactorOn")) === "true" ? postData.set("IsTwoFactorOn", "1") : postData.set("IsTwoFactorOn", "0");
    (postData.get("IsEmailVerified")) === "true" ? postData.set("IsEmailVerified", "1") : postData.set("IsEmailVerified", "0");
    postData.set("IsTermsAccepted", "1");
    $.ajax({
        type: 'POST',
        url: "/api/v1/user/adduser/",
        data: postData,
        headers: {
            'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
        },
        contentType: false,
        processData: false,
        success: function (result) {
            Swal.fire(
                'User Created',
                result,
                'success'
            ).then(LoadUsers())
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Split the error string:
            const errors = jqXHR.responseText.split(",");

            Swal.fire({
                title: 'Error!',
                text: 'User Could not be created.',
                footer: makeArrayList(errors)
            })
        }
    });

}

/* GET USER BY USERNAME - */
function GetUserByUsername(username) {
    $.ajax({
        type: "GET",
        url: "/api/v1/User/GetUserByUserName/" + username,
        dataType: "json",        
        success: function (result) {
            console.log(result);
            $("#userId").text(result.userId);
            $("#_firstname").val(result.firstname);
            $("#_middlename").val(result.middlename);
            $("#_lastname").val(result.lastname);
            $("#_email").val(result.email);
            $("#_username").val(result.username);
            $("#_phone").val(result.phone);
            // $("#_birthdate").val(result.birthday);
            $("#_birthdate").datepicker("setDate", new Date(result.birthday));
            $("#_gender").val(result.gender).trigger("chosen:updated");
            $("#_displayname").val(result.displayname);
            $("#_role").val(result.userRole);
            $("#_imgProfile").attr('src', result.profilePic);

            result.useAddress.forEach((obj, index) => {
                if (obj.type === "Billing") {
                    console.log("Billing");
                    let BillingAddress = obj;

                    for (let billingAddressObj in BillingAddress) {

                        if (BillingAddress.hasOwnProperty(billingAddressObj)) {
                            if (BillingAddress[billingAddressObj] === null) {
                                BillingAddress[billingAddressObj] = "";
                            }
                            let id = "#_" + billingAddressObj + ""; // test prop -  to check value on console.log(id)

                            if (billingAddressObj === "line1") {
                                $("#_address1").val(BillingAddress[billingAddressObj]);
                            }
                            if (billingAddressObj === "line2") {
                                $("#_address2").val(BillingAddress[billingAddressObj]);
                            }
                            if (billingAddressObj === "postalCode") {
                                $("#_postalcode").val(BillingAddress[billingAddressObj]);
                            }
                            else {
                                $(id).val(BillingAddress[billingAddressObj]);
                            }
                        }
                    }
                }
                if (obj.type === "Shipping") {
                    let ShippingAddress = obj;
                    for (let shippingAddressObj in ShippingAddress) {
                        if (ShippingAddress.hasOwnProperty(shippingAddressObj)) {
                            if (ShippingAddress[shippingAddressObj] === null) {
                                ShippingAddress[shippingAddressObj] = "";
                            }

                            let id = "#_s" + shippingAddressObj + ""; // test prop -  to check value on console.log(id)

                            if (shippingAddressObj === "line1") {
                                $("#_saddress1").val(ShippingAddress[shippingAddressObj]);
                            }
                            if (shippingAddressObj === "line2") {
                                $("#_saddress2").val(ShippingAddress[shippingAddressObj]);
                            }
                            if (shippingAddressObj === "postalCode") {
                                $("#_spostalcode").val(ShippingAddress[shippingAddressObj]);
                            }
                            else {
                                $(id).val(ShippingAddress[shippingAddressObj]);
                            }

                        }
                    }
                }
            });

            /* check if the user has country populated */
            let userCountry = (result.useAddress[0].type === "Billing") ? result.useAddress[0].country : result.useAddress[1].country;
            let userState = (result.useAddress[0].type === "Billing") ? result.useAddress[0].state : result.useAddress[1].state;
            let userShippingCountry = (result.useAddress[1].type === "Shipping") ? result.useAddress[1].country : result.useAddress[0].country;
            let userShippingState = (result.useAddress[1].type === "Shipping") ? result.useAddress[1].state : result.useAddress[0].state;


            if (userCountry !== null && userCountry !== "") {
                let country = countries.filter(function (x) { return x.name === userCountry });

                if (country.length > 0) {
                    $_billingCountryDropdown.val(country[0].id).trigger("chosen:updated");

                    let states = [];
                    if (country[0].states !== null && country[0].states !== "") {
                        states = country[0].states.split("|");

                        let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                        $.each(states, function (index, item) {
                            optionsValues += '<option value="' + item + '">' + item + '</option>';
                        });
                        $_billingStateDropdown.append(optionsValues);
                        $_billingStateDropdown.chosen();
                        $_billingStateDropdown.val(userState).trigger("chosen:updated");
                    }

                }
            }
            if (userShippingCountry !== null && userShippingCountry !== "") {
                let country = countries.filter(function (x) { return x.name === userShippingCountry });

                if (country.length > 0) {
                    $_shippingCountryDropdown.val(country[0].id).trigger("chosen:updated");

                    let states = [];
                    if (country[0].states !== null && country[0].states !== "") {
                        states = country[0].states.split("|");

                        let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                        $.each(states, function (index, item) {
                            optionsValues += '<option value="' + item + '">' + item + '</option>';
                        });
                        $_shippingStateDropdown.append(optionsValues);
                        $_shippingStateDropdown.chosen();
                        $_shippingStateDropdown.val(userShippingState).trigger("chosen:updated");
                    }

                }
            }

            /* Populate Checkbox */
            $("#EditUserModal #_IsTermsAccepted").attr("checked", result.isTermsAccepted);
            $("#EditUserModal #_IsAccountLocked").attr("checked", result.isAccountLocked);
            $("#EditUserModal #_IsEmailVerified").attr("checked", result.isEmailVerified);
            $("#EditUserModal #_IsPhoneVerified").attr("checked", result.isPhoneVerified);
            $("#EditUserModal #_IsEmployee").attr("checked", result.isEmployee);
            $("#EditUserModal #_IsTwoFactorOn").attr("checked", result.isTwoFactorOn);           
        }
    });
}

/* EDIT USER METHOD - */
function EditUser(postData) {
    (postData.get("IsEmployee")) === "true" ? postData.set("IsEmployee", "1") : postData.set("IsEmployee", "0");
    (postData.get("IsAccountLocked")) === "true" ? postData.set("IsAccountLocked", "1") : postData.set("IsAccountLocked", "0");
    (postData.get("IsPhoneVerified")) === "true" ? postData.set("IsPhoneVerified", "1") : postData.set("IsPhoneVerified", "0");
    (postData.get("IsTwoFactorOn")) === "true" ? postData.set("IsTwoFactorOn", "1") : postData.set("IsTwoFactorOn", "0");
    (postData.get("IsEmailVerified")) === "true" ? postData.set("IsEmailVerified", "1") : postData.set("IsEmailVerified", "0");
    postData.set("IsTermsAccepted", "1");
    console.log(postData.get("IsEmployee"));
    var userId = $("#userId").text();
    $.ajax({
        type: 'PUT',
        url: "/api/v1/user/edituser/" + userId,
        data: postData,
        headers: {
            'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
        },
        contentType: false,
        processData: false,
        success: function (result) {
            $('#editUserForm')[0].reset();
            $("#EditUserModal").modal('hide');
            Swal.fire(
                'User Details Edited',
                result,
                'success'
            ).then(LoadUsers())
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Split the error string:
            const errors = jqXHR.responseText.split(",");

            Swal.fire({
                title: 'Error!',
                text: 'User details Could not be edited.',
                footer: makeArrayList(errors) /* Using the extension method */
            })
        }
    });
}

/* DELETE USER METHOD - */
function confirmDeleteUser() {
    var deluser = $("#toDeleteUr").text();
    // handle deletion here
    if (deluser !== "") {
        $.ajax({
            type: "DELETE",
            url: "/api/v1/User/DeleteUser/" + deluser,
            contentType: "application/json",
            headers: {
                'X-XSRF-TOKEN': getCookie("XSRF-TOKEN"),
            },
            async: true,            
            success: function (result) {
                $("#DeleteUserModal").modal('hide');

                Swal.fire(
                    'Sucess!',
                    'User : ' + deluser + ' deleted.',
                    'success'
                );
                LoadUsers();
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

/* LOAD MODALS */
function addNewUser() {
    $("#roleError").text("");
    $("#AddUserModal").modal('show');
    $("#AddUserModal .modal-title").html("Add New User");
}

function editUserById(value) {
    let username = $(value).data('username');
    GetUserByUsername(username);
    $("#EditUserModal").modal('show');
    $("#EditUserModal .modal-title").html("Edit User");
}

function deleteUser(value) {
    var deluser = $(value).data('deluser');
    $("#DeleteUserModal").modal('show');
    $("#DeleteUserModal .modal-title").html("Delete Confirmation");
    $("#DeleteUserModal .modal-body").html("Do You Want To Delete User with Id : " + "<strong class='text-danger'><span id='toDeleteUr'>" + deluser + "</span></strong>");
}

/* TABULATOR CODE */
var table = new Tabulator("#usersTable", {
    layout: "fitColumns",      //fit columns to width of table
    responsiveLayout: "hide",  //hide columns that dont fit on the table
    tooltips: true,            //show tool tips on cells
    addRowPos: "top",          //when adding a new row, add it to the top of the table
    history: true,             //allow undo and redo actions on the table
    pagination: "local",       //paginate the data
    paginationSize: 7,         //allow 7 rows per page of data
    movableColumns: true,      //allow column order to be changed
    resizableRows: false,      //allow row order to be changed
    initialSort: [             //set the initial sort order of the data
        { column: "username", dir: "asc" }
    ],
    columns: [                //define the table columns
        { title: "Username", field: "username" },
        { title: "Firstname", field: "firstname" },
        { title: "Lastname", field: "lastname" },
        { title: "Email", field: "email" },
        {
            title: "ProfilePic", field: "profilePic", formatter: "image", formatterParams: {
                height: "50px",
                width: "50px"
            }
        },
        { title: "ProfileComplete", field: "isProfileComplete", formatter: "tickCross" },
        { title: "Active", field: "isActive", formatter: "tickCross" },

        {
            title: "Actions", align: "center", formatter: function (cell) {
                var userName = cell.getData().username;
                var firstName = cell.getData().firstname;
                var lastName = cell.getData().lastname;
                //Create the button element
                var newEditRow = "<div class='btn-group' role='group' aria-label='Perform Actions'>" +
                    "<button type='button' name='Edit'  class='btn btn-primary btn-sm' onclick='editUserById(this)' " +
                    " data-username='" + userName + "' " +
                    " data-name='" + firstName + "' " +
                    " data-normalname='" + lastName + "' " +
                    ">" +
                    "<span>" +
                    "<i class='fa fa-edit'>" +
                    "</i>" +
                    "</span>" +
                    "</button>" +
                    "<button type='button' name='Delete' data-deluser='" + userName + "'  class='btn btn-danger btn-sm' onclick='deleteUser(this)' " +
                    ">" +
                    "<span>" +
                    "<i class='fa fa-trash'>" +
                    "</i>" +
                    "</span>" +
                    "</button>" +
                    "</div>";

                return newEditRow; // return the button element
            }
        }


    ]
});

/* EXTENSION METHODS TO OPEN FILE EXPLORER TO UPLOAD IMAGE AND PREVIEW IMAGE */
function openFileExplorer(item) {
    let type = $(item).data('type');

    if (type === "editForm") {
        $(item).closest("div").find("#_profpicfile").trigger('click');
    }
    if (type === "addForm") {
        $(item).closest("div").find("#profpicfile").trigger('click');
    }

}

function PreviewImage(value) {
    let file = [];

    let type = $(value).data('type');

    // First get all the selected file and store it in a variable.

    if (type === "editForm") {
        file = document.querySelector("#_profpicfile").files;
    }
    if (type === "addForm") {
        file = document.querySelector("#profpicfile").files;
    }
    if (file.length <= 0) {
        return;
    }

    /* Make sure `file.name` matches our extensions criteria */

    if (!/\.(jpe?g|png|gif)$/i.test(file[0].name)) {
        console.log("Please upload valid image type");
    }
    console.log(file[0].name);
    var reader = new FileReader();
    reader.onload = function () {
        if (type === "editForm") {
            $("#_imgProfile").attr('src', reader.result);
        }
        if (type === "addForm") {
            $("#imgProfile").attr('src', reader.result);
        }        
    };

    /* readAsDataURL is used to read the contents of the file, when the file reading is done , the loaded event is triggered */

    reader.readAsDataURL(file[0]);
}

/* METHOD TO COPY BILLING ADDRESS TO SHIPPING ADDRESS */
var copyBillingAddress = function (value) {
    let type = $(value).data('type');
    if (type === "editForm") {
        var _addressLine1 = $("#_address1").val();
        var _addressLine2 = $("#_address2").val();
        var _unit = $("#_unit").val();
        var _country = $("#_country").val();
        var _city = $("#_city").val();
        var _state = $("#_state").val();
        var _postalCode = $("#_postalcode").val();
        var _birthdate = $("#_birthdate").val();

        $("#_saddress1").val(_addressLine1);
        $("#_saddress2").val(_addressLine2);
        $("#_sunit").val(_unit);
        $("#_scountry").val(_country);
        $("#_scity").val(_city);
        $("#_sstate").val(_state);
        $("#_spostalcode").val(_postalCode);
    }
    if (type === "addForm") {
        var addressLine1 = $("#address1").val();
        var addressLine2 = $("#address2").val();
        var unit = $("#unit").val();
        var country = $("#country").val();
        var city = $("#city").val();
        var state = $("#state").val();
        var postalCode = $("#postalcode").val();

        $("#saddress1").val(addressLine1);
        $("#saddress2").val(addressLine2);
        $("#sunit").val(unit);
        $("#scountry").val(country);
        $("#scity").val(city);
        $("#sstate").val(state);
        $("#spostalcode").val(postalCode);
    }

};

/* METHOD TO GENERATE RANDOM PASSWORD */
function generatePassword() {
    var randPass = password_generator(6);
    $("#password").val(randPass).focus();
}

/* EXTENSION METHOD TO MAKE ERROR LIST FROM ERROR RESPONSE */
function makeArrayList(array) {
    // Create the list element:
    let list = document.createElement('ul');

    // add class to element
    list.className = "list-group";

    for (let i = 0; i < array.length; i++) {
        // Create the list item:
        let item = document.createElement('li');

        item.className = "list-group-item";
        // Set its contents: => g in replace() means case sensitive
        item.appendChild(document.createTextNode(array[i].replace(/[\[\]"]/g, '')));

        // Add it to the list:
        list.appendChild(item);
    }

    // Finally, return the constructed list:
    return list;

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