//$(document).ready(function () {
//    $('#NotCorpDatatable').dataTable({
//    });
//});
var table = new DataTable('#NotCorpDatatable', {
    info: false,
    lengthChange: true,
    stateSave: true,
    columnDefs: {
        targets: 'no-sort',
        orderable: false
    },
    dom: 'ftp',
    language: {
        search: "",
        searchPlaceholder: "Buscar...",
        lengthMenu: "_MENU_  Registros por página",
        emptyTable: "No hay datos para mostrar",
        infoEmpty: "",
        paginate: {
            previous: "Anterior",
            next: "Siguiente",
        },
        zeroRecords: "No se encontraron coincidencias",
        info: "Mostrando _START_ a _END_ de _TOTAL_ registros"
    },
    
});

var idNotEliminar = 0;
var idNotModificar = 0;
function ModificarNotificacionCorporativa(idNot, notificacion) {
    idNotModificar = idNot;
    document.querySelector('#titleModal').innerHTML = "Modificar Notificación Corporativa";
    document.querySelector("#txtNotificacion").value = notificacion;
    $('#modalNuevaNotCorp').modal('show');
}
function AgregarNotificacionCorporativa(idNot) {
    document.querySelector('#titleModal').innerHTML = "Nueva Notificación Corporativa";
    document.querySelector("#txtNotificacion").value = "";
    $('#modalNuevaNotCorp').modal('show');
}

function GuardarNotificacionCorporativa() {
    let input = document.querySelector("#txtNotificacion").value;
    let mensajes = "";

    $('#modal-procesar-respuesta').modal('show');    

    if (input.length == 0) {
        mensajes = "Notificación vacía. Debe agregar el contenido de la notificación";
    }
    if (input.length < 5 && mensajes == "") {
        mensajes = "La notificación ingresada debe tener mínimo 5 caracteres";
    }
    if (!isNaN(input) && mensajes == "") {
        mensajes = "No puede agregar una notificación que solo contenga números";
    }
    if (input.length > 55 && mensajes == "") {
        mensajes = "Superó la cantidad de caracteres permitidos (55).";
    }
    if (mensajes == "") {
        $("#modal-procesar-respuesta .modal-body").attr(
            "style", "background-color: #78b36d"
        );        
        $.ajax(
            {
                type: 'POST',
                url: '/NotificacionesCorporativas/NuevaNotificacionCorporativa',
                type: "post",
                data: { asunto: input, idNotificacion: idNotModificar },
                success: function () {
                    $('#modalNuevaNotCorp').modal('hide');
                    $("#modal-procesar-respuesta .modal-body label").html("Operación exitosa");
                    setTimeout(function () {
                        $('#modal-procesar-respuesta').modal('hide');
                        location.reload();
                    }, 1500);
                    idNotModificar: 0;
                },
            }
        );
    }
    else {
        $("#modal-procesar-respuesta .modal-body label").html(mensajes);
        setTimeout(function () { $('#modal-procesar-respuesta').modal('hide') }, 2000);
    }
}

function CerrarModal() {
    $('#modalNuevaNotCorp').modal('hide');
}
function CerrarModalError() {
    $('#modal-danger-delete-notificacionCorp').modal('hide');
}
function CerrarModalErrorValidacion() {
    $('#modal-danger-error').modal('hide');
}

function EliminarNotificacionCorporativa(idNotCorp) {
    idNotEliminar = idNotCorp;
    $('#modal-danger-delete-notificacionCorp').modal('show');
}

function OnSuccessEliminarNotificacionCorp() {
    $('#modal-procesar-respuesta').modal('show');   

    $("#modal-procesar-respuesta .modal-body").attr(
        "style", "background-color: #78b36d"
    ); 

    $.ajax(
        {
            type: 'POST',
            url: '/NotificacionesCorporativas/EliminarNotificacionCorporativa',
            type: "post",
            data: { idNotCorp: idNotEliminar },
            success: function () {
                $('#modal-danger-delete-notificacionCorp').modal('hide');
                $("#modal-procesar-respuesta .modal-body label").html("Operación exitosa");
                setTimeout(function () {
                    $('#modal-procesar-respuesta').modal('hide');
                    location.reload();
                }, 2000);
            },
        }
    );
}