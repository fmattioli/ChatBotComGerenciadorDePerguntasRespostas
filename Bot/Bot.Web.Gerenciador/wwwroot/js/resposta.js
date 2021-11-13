function openModal(perguntaNome, perguntaId) {
    $('.modal').modal({
        dismissble: true
    });

    $('#modalResposta').modal('open');
    $('#pergunta').val(perguntaNome);
    $("#btnAdicionarResposta").on('click', function () {
        if ($('#resposta').val() === "") {
            $('#resposta').focus();
            return;
        }

        if ($('#resposta').val().length <= 6) {
            M.toast({
                html: "Insira uma resposta com mais de 5 caracteres",
                classes: 'black darken-4 rounded',
            });
            return;
        }

        var intencaoLuisViewModel = {
            "Resposta": $('#resposta').val(),
            "Pergunta": {
                "Id": perguntaId
            }
        };

        console.log(intencaoLuisViewModel);

        $.ajax({
            url: "/Resposta/AdicionarResposta/",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(intencaoLuisViewModel),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.msgErro === "") {
                    window.location.reload(true);
                }

                M.toast({
                    html: response.msgErro,
                    classes: 'black darken-4 rounded',
                });

            },
            error: function (xhr, textStatus, errorThrown) {
                M.toast({
                    html: "Erro ao adicionar resposta",
                    classes: 'black darken-4 rounded',
                });
            }
        });

    })

}

function desativarResposta(Id) {
    $.ajax({
        method: "POST",
        url: "/Resposta/DesativarResposta/",
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
                html: "Erro ao excluir usuario",
                classes: 'black darken-4 rounded',
            });
        }
    });
}



