var createObj = {
    domain : "/khao-sat",
    createQuestion: function () {
        var listAnswer = [];
        if (ValidateForm(frmCreateConfig)) {
            return;
        }
        $("#boxAnswer").find(".answer-item").each(function () {
            var answer = {
                text: $(this).find("input[type=text]").val(),
                rightAnswer: $(this).find("input[type=radio]").prop("checked")
            }
            listAnswer.push(answer);
        });
        var position = $("#isUpdate").val();
        console.log(position);
        if (position == -1) {

            var data = {
                id: 0,
                description: $("#txtDescriptionQuestion").val(),
                type: $("#cbType").val(),
                //other: $("#ckOther").prop("checked"),
                answers: listAnswer
            }
            listQuestion.push(data);
        }
        else {
            listQuestion[position].description = $("#txtDescriptionQuestion").val();
            listQuestion[position].type = $("#cbType").val();
            //listQuestion[position].other = $("#ckOther").prop("checked");
            listQuestion[position].answers = listAnswer;
        }
        setDataSurvey();
        hideContentModal();
    },
    addAnswer: function () {
        var text = '<div class="row answer-item form-group d-flex align-items-center" id="row_' + stt + '">';
        text += '<input class="form-control col-sm-9" type="text" placeholder="Lựa chọn"><label  class="col-sm-2"> <input type="radio" name="right-answer" /> Đáp án</label><a onclick="deleteAnswer(' + stt + ')" style="margin:auto" href="javascript:;" class="text-right"><i class="fas fa-times"></i></a> </div>';
        $("#boxAnswer").append(text);
        stt++;
    }
}

