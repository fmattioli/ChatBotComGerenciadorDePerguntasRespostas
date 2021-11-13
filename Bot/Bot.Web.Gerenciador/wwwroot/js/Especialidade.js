function adicionarEspecialidade() {

    if ($('#especialidade').val() === undefined || $('#descricao').val() === "") {
        $('#especialidade').focus();
        $('#descricao').focus();
        return;
    }

    $.ajax({
        url: "/Especialidade/AdicionarEspecialidade/",
        type: "POST",
        dataType: "json",
        data: JSON.stringify({
            Nome: $('#especialidade').val(),
            Descricao: $('#descricao').val()
        }),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.msgErro === "") {
                M.toast({
                    html: "Especialidade inserida com sucesso!",
                    classes: 'black darken-4 rounded',
                });

                $('#especialidadesGrid tbody').html(response.htmlGrid);
                $('#especialidadeForm').trigger("reset");
                return;
            }

            M.toast({
                html: response.msgErro,
                classes: 'black darken-4 rounded',
            });

        },
        error: function (xhr, textStatus, errorThrown) {
            M.toast({
                html: "Erro ao adicionar especialidade",
                classes: 'black darken-4 rounded',
            });
        }
    });
}


function excluirEspecialidade(EspecialdiadeId) {

    $('.modal').modal({
        dismissble: true
    });
    $('#modal').modal('open');
    $(".texto").text("Deseja realmente excluir?");
   
    $(".btnExcluir").on('click', function () {
        $.ajax({
            method: "POST",
            url: "/Especialidade/ExcluirEspecialidade/",
            dataType: "json",
            data: { id: EspecialdiadeId },
            success: function (response) {
                if (response.msgErro === "") {
                    M.toast({
                        html: "Especialidade excluída com sucesso!",
                        classes: 'black darken-4 rounded',
                    });

                    $('#especialidadesGrid tbody').html(response.htmlGrid);
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
                    html: "Erro ao excluir especialidade",
                    classes: 'black darken-4 rounded',
                });
            }
        })
    })

}



