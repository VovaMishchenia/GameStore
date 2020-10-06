function setFilter(event, type) {
    //location = `/Game/Index?type=${type}&name=${event.target.value}`;

    $("#games").load(`/Game/Filter?type=${type}&name=${encodeURIComponent(event.target.value)}`);
}

function order(event) {
    if (event.target.id == "toLow") {
        $("#games").load(`/Game/OrderByLow`);
    }
    else if (event.target.id == "toHeigh") {
        $("#games").load(`/Game/OrderByHeigh`);

    }
} function setSearch(event) {
    let search_Field = document.getElementById("search_Field");
    $("#games").load(`/Game/Search?name=${encodeURIComponent(search_Field.value)}`);

}

function setRadio(event) {
    let form_group = document.getElementById("image-group");

    if (event.target.id == "PCRadio") {
        while (form_group.firstChild) {
            form_group.removeChild(form_group.firstChild);
        }
        let div = document.createElement("div");
        div.className = "col-md-10";
        let inputFile = document.createElement("input");
        inputFile.type = "file";
        inputFile.name = "image";
        inputFile.id = "image";
        inputFile.className = "form-control";
        inputFile.required = true;
        div.append(inputFile);
        form_group.append(div);
    }
    else if (event.target.id == "linkRadio") {
        while (form_group.firstChild) {
            form_group.removeChild(form_group.firstChild);
        }
        let div2 = document.createElement("div");
        div2.className = "col-md-10";
        let inputText = document.createElement("input");
        inputText.type = "text";
        inputText.name = "image";
        inputText.id = "image";
        inputText.className = "form-control";
        inputText.required = true;
        div2.append(inputText);
        form_group.append(div2);
    }
}
function Plus(name) {

    let input = document.getElementById(name + "_input");
    if (parseInt(input.max) >= (parseInt(input.value) + 1)) {
        input.value++;
        $("#cart").load(`/Auth/RaiseAmount?name=${encodeURIComponent(name)}`);
    }
}
function Minus(name) {

    let input = document.getElementById(name + "_input");
    if (1 < parseInt(input.value)) {
        input.value--;
        $("#cart").load(`/Auth/ReduceAmount?name=${encodeURIComponent(name)}`);
    }
}
function DeleteUnit(name) {
    $("#cart").load(`/Auth/DeleteCartUnit?name=${encodeURIComponent(name)}`);
}
