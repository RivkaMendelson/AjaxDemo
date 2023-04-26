$(() => {
    const addModal = new bootstrap.Modal($('#add-modal')[0]);
    const editModal = new bootstrap.Modal($('#edit-modal')[0]);



    function refreshTable() {
        $("tbody").empty();
        $.get('/home/getpeople', function (people) {
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td id="firstName">${person.firstName}</td>
            <td id="lastName">${person.lastName}</td>
            <td id="age">${person.age}</td>
            <td><button class="btn btn-warning" id="editButton" data-id="${person.id}">Edit</button></td>
            <td><button class="btn btn-danger" id="deleteButton" data-id="${person.id}">Delete</button></td>

</tr>`)
            });
        });
    }

    refreshTable();

    $("#add-person").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        addModal.show();
    });

    $("#save-person").on('click', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();


        //const person = {
        //    firstName,
        //    lastName,
        //    age
        //};

        $.post('/home/addperson', { firstName, lastName, age }, function () {
            addModal.hide();
            refreshTable();
        });


    });

    $('tbody').on('click', '#deleteButton', function () {

        const id = $(this).data('id');

        $.post('/home/delete', {id} , function () {
            refreshTable();
        });

    });

    $('tbody').on('click', '#editButton', function () {

        var id = $(this).data("id");
        var firstName = $(this).closest("tr").find("#firstName").text();
        var lastName = $(this).closest("tr").find("#lastName").text();
        var age = $(this).closest("tr").find("#age").text();
        editModal.show();

        console.log(id, firstName, lastName, age);
        $("#firstNameInput").val(firstName);
        $("#lastNameInput").val(lastName);
        $("#ageInput").val(age);
        $("#personId").val(id);


    });


    $('#update-person').on('click', function () {

        const id = $("#personId").val();
        const firstName = $("#firstNameInput").val();
        const lastName = $("#lastNameInput").val();
        const age = $("#ageInput").val();

        editModal.hide();

        $.post('/home/edit', { id, firstName, lastName, age }, function () {
            refreshTable();
        });

    });
   

})