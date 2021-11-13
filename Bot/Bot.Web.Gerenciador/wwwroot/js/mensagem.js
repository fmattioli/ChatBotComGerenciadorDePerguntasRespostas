function openModal(especialidadeId, mensagem) {
    $('.modal').modal({
        dismissble: true
    });
    $('#modalMensagem').modal('open');
    $('#especialidade').val(mensagem);

    $("#btnAdicionarMensagem").on('click', function () {
        if ($('#mensagem').val() === "") {
            $('#mensagem').focus();
            return;
        }

        if ($('#mensagem').val().length <= 3) {
            M.toast({
                html: "Insira uma pergunta com mais de 3 caracteres",
                classes: 'black darken-4 rounded',
            });
            return;
        }

        var intencaoLuisViewModel = {
            "Mensagem": $('#mensagem').val(),
            "Especialidade": {
                "Id": especialidadeId
            }
        };

        console.log(intencaoLuisViewModel);

        $.ajax({
            url: "/MensagemInicio/AdicionarMensagem/",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(intencaoLuisViewModel),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.msgErro === "") {
                    window.location.reload(true);
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

    })
}

function desativarmensagem(Id) {
    $.ajax({
        method: "POST",
        url: "/MensagemInicio/DesativarMensagem/",
        dataType: "json",
        data: { id: Id },
        success: function (response) {
            if (response.msgErro === "") {
                location.reload(true);
                return;
            }

            M.toast({
                html: response.msgErro,
                classes: 'black darken-4 rounded',
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            M.toast({
                html: "Erro ao excluir mensagem",
                classes: 'black darken-4 rounded',
            });
        }
    });
}



