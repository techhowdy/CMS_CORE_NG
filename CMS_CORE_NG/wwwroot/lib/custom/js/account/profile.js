let countries = [];

let BillingAddress = {
    addressId: "",
    line1: "",
    line2: "",
    country: "",
    city: "",
    state: "",
    postalCode: "",
    type: "",
    unit: "",
    userId: ""
};

let ShippingAddress = {
    addressId: "",
    line1: "",
    line2: "",
    country: "",
    city: "",
    state: "",
    postalCode: "",
    type: "",
    unit: "",
    userId: ""
};

let updateProfileForm =
{
    userid: "",
    email: "",
    username: "",
    phone: "",
    birthdate: "",
    gender: "",
    displayname: "",
    address1: "",
    address2: "",
    country: "",
    city: "",
    state: "",
    postalcode: "",
    saddress1: "",
    saddress2: "",
    scountry: "",
    scity: "",
    sstate: "",
    spostalcode: "",
    profpicfile: "",
    firstname: "",
    lastname: "",
    isTwoFactorOn: "",
    isPhoneVerified: "",
    isEmailVerified: "",
    isTermsAccepted: "",
    unit: "",
    sunit: ""
};

function GetUserByUsername(username) {

    $.ajax({
        type: "GET",
        url: "/api/v1/Profile/GetUserProfile/" + username,
        dataType: "json",
        success: function (result)
        {
            let middlename = (result.middlename !== null) ? result.middlename.toUpperCase() : "";
            $("#fullName").text(result.firstname.toUpperCase() + " " + middlename + " " + result.lastname.toUpperCase());
            $("#firstname").val(result.firstname);
            $("#middlename").val(result.middlename);
            $("#appUserRole").text(result.userRole);
            $("#lastname").val(result.lastname);
            $("#email").val(result.email);
            $("#username").val(result.username);
            $("#phone").val(result.phone);
            $("#displayname").val(result.displayname);

            let gender = (result.gender == null) ? "Select Gender" : result.gender;
            $("#gender").val(gender).trigger("chosen:updated");

            if (result.birthday == null || result.birthday === "") {
                $("#birthdate").datepicker("setDate", null)
            }
            else {
                $("#birthdate").datepicker("setDate", new Date(result.birthday));
            }        

            $("#userId").val(result.userId);

            result.useAddress.forEach((obj, index) => {
                if (obj.type === "Billing") {
                    
                    BillingAddress = obj;

                    for (let billingAddressObj in BillingAddress) {

                        if (BillingAddress.hasOwnProperty(billingAddressObj)) {
                            if (BillingAddress[billingAddressObj] === null) {
                                BillingAddress[billingAddressObj] = "";
                            }
                            let id = "#" + billingAddressObj + ""; 

                            if (billingAddressObj === "line1") {
                                $("#address1").val(BillingAddress[billingAddressObj]);
                            }
                            if (billingAddressObj === "line2") {
                                $("#address2").val(BillingAddress[billingAddressObj]);
                            }
                            if (billingAddressObj === "postalCode") {
                                $("#postalcode").val(BillingAddress[billingAddressObj]);
                            }
                            else {
                                $(id).val(BillingAddress[billingAddressObj]);
                            }
                        }
                    }
                }
                if (obj.type === "Shipping") {
                    ShippingAddress = obj;
                    for (let shippingAddressObj in ShippingAddress) {
                        if (ShippingAddress.hasOwnProperty(shippingAddressObj)) {
                            if (ShippingAddress[shippingAddressObj] === null) {
                                ShippingAddress[shippingAddressObj] = "";
                            }

                            let id = "#s" + shippingAddressObj + ""; 

                            if (shippingAddressObj === "line1") {
                                $("#saddress1").val(ShippingAddress[shippingAddressObj]);
                            }
                            if (shippingAddressObj === "line2") {
                                $("#saddress2").val(ShippingAddress[shippingAddressObj]);
                            }
                            if (shippingAddressObj === "postalCode") {
                                $("#spostalcode").val(ShippingAddress[shippingAddressObj]);
                            }
                            else {
                                $(id).val(ShippingAddress[shippingAddressObj]);
                            }

                        }
                    }
                }
            });

            /* check if the user has country populated */
            let userCountry = result.useAddress[0].country;
            let userState = result.useAddress[0].state;
            let userShippingCountry = result.useAddress[1].country;
            let userShippingState = result.useAddress[1].state;


            if (userCountry !== null && userCountry !== "") {
                let country = countries.filter(function (x) { return x.name === userCountry });

                if (country.length > 0) {
                    $("#country").val(country[0].id).trigger("chosen:updated");

                    let states = [];
                    if (country[0].states !== null && country[0].states !== "") {
                        states = country[0].states.split("|");

                        let $billingStateDropdown = $("#state");

                        let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                        $.each(states, function (index, item) {
                            optionsValues += '<option value="' + item + '">' + item + '</option>';
                        });
                        $billingStateDropdown.append(optionsValues);
                        $billingStateDropdown.chosen();
                        $("#state").val(userState).trigger("chosen:updated");
                    }

                }
            }
            if (userShippingCountry !== null && userShippingCountry !== "") {
                let country = countries.filter(function (x) { return x.name === userShippingCountry });

                if (country.length > 0) {
                    $("#scountry").val(country[0].id).trigger("chosen:updated");

                    let states = [];
                    if (country[0].states !== null && country[0].states !== "") {
                        states = country[0].states.split("|");

                        let $shippingStateDropdown = $("#sstate");

                        let optionsValues = '<option value="Select State" disabled selected>Select</option>';
                        $.each(states, function (index, item) {
                            optionsValues += '<option value="' + item + '">' + item + '</option>';
                        });
                        $shippingStateDropdown.append(optionsValues);
                        $shippingStateDropdown.chosen();
                        $("#sstate").val(userShippingState).trigger("chosen:updated");
                    }

                }
            }

            $("#isTwoFactorOn").prop('checked', result.isTwoFactorOn);
            $("#isEmailVerified").prop('checked', result.isEmailVerified);
            $("#isPhoneVerified").prop('checked', result.isPhoneVerified);
            $("#isTermsAccepted").prop('checked', result.isTermsAccepted);

            let profilePic = (result.profilePic == null) ? "/uploads/user/profile/default/profile.jpeg" : result.profilePic;
            $("#imgProfile").attr('src', profilePic);

        },
        error: function (err) {
            console.log(err)
        }
    });
}

function UpdateUser(data)
{
    const formData = new FormData();

    for (const key of Object.keys(data)) {
        const value = data[key];
        formData.append(key, value);
    }

    Swal.fire({
        title: 'Enter your password',
        input: 'password',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Update Profile',
        showLoaderOnConfirm: true,
        preConfirm: (password) => {
            formData.append("password", password);
            $.ajax({
                type: 'POST',
                url: "/api/v1/Profile/UpdateProfile",
                data: formData,
                contentType: false,
                processData: false,                
                headers: {                    
                    'Accept': 'multipart/form-data',
                    'X-XSRF-TOKEN': getCookie("XSRF-TOKEN")
                },
                success: function (result) {
                    Swal.fire(
                        'Profile Updated',
                        result,
                        'success'
                    ).then(GetUserByUsername(data.username))
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Split the error string:
                    const errors = jqXHR.responseText.split(",");

                    Swal.fire({
                        title: 'Error!',
                        text: 'Profile Could not be updated'
                    })
                }
            });
        }
    })
}

function triggerInput() {
    $("#profpicfile").trigger('click');
}

function onFileChanged(event) {
    if (event.files && event.files[0]) {
        let reader = new FileReader();
        let file = event.files[0];
        updateProfileForm.profpicfile = file;
        reader.readAsDataURL(file);
        reader.onload = () => {
            $("#profpic").find('img').attr('src', reader.result);
        };
    }
}

function getCountries() {
    $.ajax({
        type: "GET",
        url: "/api/v1/Country/GetCountries",
        dataType: "json",
        async: false,        
        success: function (result) {
            countries = result;
            if (countries) {
                let $billingCountryDropdown = $("#country");
                let $shippingCountryDropdown = $("#scountry");
                let optionsValues = '<option value="Select Country" disabled selected>Select</option>';
                $.each(countries, function (index, item) {
                    optionsValues += '<option value="' + item.id + '">' + item.flag + ' ' + item.name + '</option>';
                });
                $billingCountryDropdown.append(optionsValues);
                $shippingCountryDropdown.append(optionsValues);
                $billingCountryDropdown.chosen();
                $shippingCountryDropdown.chosen();
            }
        }
    });
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