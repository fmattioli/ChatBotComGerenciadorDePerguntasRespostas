function openModal(pergunta, IntencaoId) {
    $('.modal').modal({
        dismissble: true
    });
    $('#modal').modal('open');
    $('#intencao').val(pergunta);

    $("#btnAdicionarPergunta").on('click', function () {
        if ($('#pergunta').val() === "" || $('#pergunta').val().length <= 8) {
            $('#pergunta').focus();
            return;
        }

        if ($('#pergunta').val().length <= 8) {
            M.toast({
                html: "Insira uma intenção com mais de 8 caracteres",
                classes: 'black darken-4 rounded',
            });
            return;
        }

        var intencaoLuisViewModel = {
            "TextoPergunta": $('#pergunta').val(),
            "Intencao": {
                "Id": IntencaoId
            }
        };

        console.log(intencaoLuisViewModel);

        $.ajax({
            url: "/Pergunta/AdicionarPergunta/",
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
                    html: "Erro ao adicionar usuário",
                    classes: 'black darken-4 rounded',
                });
            }
        });

    })
}

function desativarPergunta(Id) {
    $.ajax({
        method: "POST",
        url: "/Pergunta/DesativarPergunta/",
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



