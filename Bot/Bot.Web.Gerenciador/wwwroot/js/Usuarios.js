$("#senha").on("focusout", function (e) {
    if ($(this).val() != $("#confirmarsenha").val()) {
        $("#confirmarsenha").removeClass("valid").addClass("invalid");
    } else {
        $("#confirmarsenha").removeClass("invalid").addClass("valid");
    }
});

$("#confirmarsenha").on("keyup", function (e) {
    if ($("#senha").val() != $(this).val()) {
        $(this).removeClass("valid").addClass("invalid");
    } else {
        $(this).removeClass("invalid").addClass("valid");
    }
});


function inserirUsuario() {
    if ($('#nome').val() === "") {
        $('#nome').focus();
        return;
    }

    if ($('#email').val() === "") {
        $('#email').focus();
        return;
    }

    if ($('#senha').val() === "") {
        $('#senha').focus();
        return;
    }

    if ($('#confirmarsenha').val() === "") {
        $('#confirmarsenha').focus();
        return;
    }

    $.ajax({
        url: "/Usuarios/Cadastrar/",
        type: "POST",
        dataType: "json",
        data: JSON.stringify({
            Nome: $('#nome').val(),
            Email: $('#email').val(),
            Senha: $('#senha').val(),
            SenhaConfirmada: $('#confirmarsenha').val()
        }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.msgErro === "") {
                M.toast({
                    html: "Usuário inserido com sucesso!",
                    classes: 'black darken-4 rounded',
                });

                $('#usuariosGrid tbody').html(response.htmlGrid);
                $('#formUsuario').trigger("reset");
                return;
            }

            M.toast({
                html: response.msgErro,
                classes: 'black darken-4 rounded',
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            M.toast({
                html: "Erro ao adicionar usuário",
                classes: 'black darken-4 rounded',
            });
        }
    });
}

function excluirUsuario(usuarioId) {
    $('.modal').modal({
        dismissble: true
    });
    $('#modal').modal('open');
    $(".texto").text("Deseja realmente excluir?");

    $(".btnExcluir").on('click', function () {
        $.ajax({
            method: "POST",
            url: "/Usuarios/ExcluirUsuario/",
            dataType: "json",
            data: { id: usuarioId },
            success: function (response) {
                if (response.msgErro === "") {
                    M.toast({
                        html: "Usuário excluído com sucesso!",
                        classes: 'black darken-4 rounded',
                    });

                    $('#usuariosGrid tbody').html(response.htmlGrid);
                    $('#modal').modal('close');
                    return;
                }

                M.toast({
                    html: response.msgErro,
                    classes: 'black darken-4 rounded',
                });

            },
            error: function (xhr, textStatus, errorThrown) {
                M.toast({
                    html: "Erro ao excluir usuario",
                    classes: 'black darken-4 rounded',
                });
            }
        })
    })

}

