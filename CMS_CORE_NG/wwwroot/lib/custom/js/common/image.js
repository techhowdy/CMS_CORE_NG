// Function to read uploaded files and generate a preview

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
    // Make sure `file.name` matches our extensions criteria
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
    // readAsDataURL is used to read the contents of the file, when the file reading is done , the loaded event is triggered
    reader.readAsDataURL(file[0]);
}

// Function to open file explorer when change phote button is clicked

function openFileExplorer(item) {
    let type = $(item).data('type');

    if (type === "editForm") {
        $(item).closest("div").find("#_profpicfile").trigger('click');
    }
    if (type === "addForm") {
        $(item).closest("div").find("#profpicfile").trigger('click');
    }

}
