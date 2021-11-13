function openModal(especialidadeNome, especialidadeId) {
    $('.modal').modal({
        dismissble: true
    });
    $('#modal').modal('open');
    $('#especialidade').val(especialidadeNome);
    $("#btnAdicionarIntencao").on('click', function () {
        if ($('#intencao').val() === "") {
            $('#intencao').focus();
            return;
        }

        if ($('#intencao').val().length <= 4) {
            M.toast({
                html: "Insira uma intenção com mais de 4 caracteres",
                classes: 'black darken-4 rounded',
            });
            return;
        }

        var intencaoLuisViewModel = {
            "Intencao": $('#intencao').val(),
            "Especialidade": {
                "Id": especialidadeId
            }
        };

        console.log(intencaoLuisViewModel);

        $.ajax({
            url: "/Intencao/AdicionarIntencao/",
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

function desativarIntencao(Id) {
    $.ajax({
        method: "POST",
        url: "/Intencao/DesativarIntencao/",
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



