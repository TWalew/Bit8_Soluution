function deleteDiscipline(discipline, row) {
	if (!!discipline.Score) {
		alert("Only disciplines without scores can be deleted!");
	} else {
		$.ajax({
			url: "/Disciplines/Delete?id=" + discipline.id,
			method: "DELETE",
			contentType: "application/json",
		})
			.done(function (res) {
				if (!!res.ErrorMessage) {
					alert(res.ErrorMessage);
				} else {
					if (!!row) {
						row.remove().draw();
					} else {
						location.reload(true);
					}
				}
			})
			.fail(function (err) {
				console.error(err.responseText);
				alert("Error deleting discipline!");
			});
	}
}

function editDiscipline(discipline, btn) {
	var editMode = btn.html() === "Edit";
	var input = btn.prev("input");
	if (editMode) {
		var oldVal = $(input).val();
		btn.html("Done");
		$(input).removeAttr("readonly").attr("value", "").focus();
	} else {
		$.ajax({
			url:
				"/Disciplines/Edit?id=" +
				discipline.id +
				"&professor=" +
				$(input).val(),
			method: "PUT",
			contentType: "application/json",
		})
			.done(function (res) {
				if (!!res.ErrorMessage) {
					alert(res.ErrorMessage);
					$(input).val(oldVal);
				} else {
				}
			})
			.fail(function (err) {
				console.error(err.responseText);
				alert("Error editing discipline!");
				$(input).val(oldVal);
			})
			.always(function () {
				btn.html("Edit");
				$(input).attr("readonly", "readonly");
			});
	}
}

function onDatatableDataLoad(data) {
	console.log(data);
	// if (!!data.ErrorMessage) {
	//     console.error(data.ErrorMessage);
	// }

	return data;
}

function renderDisciplineInTableCell(
	result,
	semester,
	index,
	value,
	studentId
) {
	if (index === 0) {
		result =
			result +
			'<p class="font-italic text-info" style="margin-top:10px;">Disciplines for ' +
			semester.name +
			":</p>";
		result =
			result +
			'<div class="row"><div class="col-3 font-weight-bold">Name</div><div class="col-3 font-weight-bold">Professor</div><div class="col-3 font-weight-bold">Score</div></div>';
	}
	var deleteBtnId =
		"delete-btn-" + value.id + "_" + semester.IdSemester;
	var addBtnId = "add-btn-" + value.id + "_" + semester.IdSemester;
	var addDisciplineFormId =
		"add-form-" + value.id + "_" + semester.IdSemester;
	var addDisciplineBtnId =
		"add-discipline-btn-" + value.IdDiscipline + "_" + semester.IdSemester;
	if (studentId) {
		// fix for unique ids
		deleteBtnId += "_" + studentId;
		addBtnId += "_" + studentId;
		addDisciplineFormId += "_" + studentId;
		addDisciplineBtnId += "_" + studentId;
	}
	result =
		result +
		('<div class="row">' +
			'<div class="col-3">' +
			value.name +
			"</div>" +
			'<div class="col-3">' +
			value.ProfessorName +
			"</div>" +
			'<div class="col-3">' +
			value.Score +
			"</div>" +
			'<div class="col-3"><button type="button" id="' +
			deleteBtnId +
			'">Delete</button></div>' +
			"</div>");

	$("#" + deleteBtnId).on("click", function (e) {
		deleteDiscipline(value);
	});
	if (index === semester.Disciplines.length - 1) {
		result += getAddDisciplineUI(
			addBtnId,
			addDisciplineFormId,
			addDisciplineBtnId,
			semester.IdSemester
		);
	}

	return result;
}

function getAddDisciplineUI(
	addBtnId,
	addDisciplineFormId,
	addDisciplineBtnId,
	semesterId
) {
	var result =
		'<div class="row add-btn-wrapper" style="padding-left:10px;"><button class="col-9 btn btn-success btn-sm" type="button" id="' +
		addBtnId +
		'">Add Discipline</button></div>';
	result =
		result +
		'<form id="' +
		addDisciplineFormId +
		'" style="display:none" >' +
		'<div class="form-group">' +
		"<label>Name</label>" +
		'<input type="text" class="form-control" name="name" placeholder="Discipline name">' +
		"</div>" +
		'<div class="form-group">' +
		"<label>Professor Name</label>" +
		'<input type="text" class="form-control" name="professor" placeholder="Professor name">' +
		"</div>" +
		'<div class="form-group">' +
		"<label>Score</label>" +
		'<input type="number" class="form-control" name="score" placeholder="Score">' +
		"</div>" +
		'<button type="button" id="' +
		addDisciplineBtnId +
		'" class="btn btn-primary">Submit</button>' +
		"</form>";
	$("#" + addBtnId).on("click", function (e) {
		$("#" + addDisciplineFormId).show();
	});
	$("#" + addDisciplineBtnId).on("click", function (e) {
		var discName = $(
			"#" + addDisciplineFormId + ' input[name="name"]'
		).val();
		var profName = $(
			"#" + addDisciplineFormId + ' input[name="professor"]'
		).val();
		var score = $("#" + addDisciplineFormId + ' input[name="score"]').val();

		postCreateDiscipline(discName, profName, semesterId, score);
	});

	return result;
}

function postCreateDiscipline(
	disciplineName,
	professorName,
	semesterId,
	score
) {
	var url =
		"/Disciplines/Create?name=" +
		disciplineName +
		"&professor=" +
		professorName;
	if (semesterId) {
		url += "&semesterId=" + semesterId;
	}
	if (score) {
		url += "&score=" + score;
	}
	$.ajax({
		url: url,
		method: "POST",
		contentType: "application/json",
	})
		.done(function (res) {
			if (!!res.ErrorMessage) {
				alert(res.ErrorMessage);
			} else {
				location.reload(true);
			}
		})
		.fail(function (err) {
			console.error(err.responseText);
			alert("Error creating discipline");
		});
}

function postCreateSemester(studentId, semesterName, startDate, endDate) {
	if (!semesterName) {
		return alert("Semester must have a name!");
	}
	if (!startDate) {
		return alert("Semester must have a start date!");
	}
	if (!endDate) {
		return alert("Semester must have an end date!");
	}
	var postUrl =
		"/Semesters/Create?name=" +
		semesterName +
		"&startDate=" +
		startDate +
		"&endDate=" +
		endDate;
	if (studentId) {
		postUrl += "&studentId=" + studentId;
	}
	$.ajax({
		url: postUrl,
		method: "POST",
		contentType: "application/json",
	})
		.done(function (res) {
			if (!!res.ErrorMessage) {
				alert(res.ErrorMessage);
			} else {
				location.reload(true);
			}
		})
		.fail(function (err) {
			console.error(err.responseText);
			alert("Error creating semester");
		});
}

function getStudentsDatatableOptions(sourceUrl) {
	return {
		ajax: {
			url: sourceUrl,
			dataSrc: onDatatableDataLoad,
		},
		columns: [
			{
				data: "IdStudent",
				width: "5%",
			},
			{
				data: "FirstName",
				width: "12%",
			},
			{
				data: "LastName",
				width: "13%",
			},
			{
				data: "DateOfBirth",
				width: "10%",
			},
			{
				data: "Semesters",
				width: "60%",
				render: function (data, type, row, meta) {
					var result = "";
					result = getCreateSemesterUI(result, row);
					$.each(row.Semesters, function (index, value) {
						result =
							result +
							('<div class="row">' +
								'<div class="col-4 font-weight-bold">Name</div>' +
								'<div class="col-4 font-weight-bold">Start date</div>' +
								'<div class="col-4 font-weight-bold">End date</div>' +
								"</div>");
						result =
							result +
							('<div class="row">' +
								'<div class="col-4">' +
								value.name +
								"</div>" +
								'<div class="col-4">' +
								value.StartDate +
								"</div>" +
								'<div class="col-4">' +
								value.EndDate +
								"</div>" +
								"</div>");

						if (value.Disciplines.length === 0) {
							result += getAddDisciplineUI(
								"empty-semester-add-btn" + value.IdSemester,
								"empty-semester-form-" + value.IdSemester,
								"empty-semester-btn-" + value.IdSemester,
								value.IdSemester
							);
						}

						$.each(value.Disciplines, function (i, val) {
							result = renderDisciplineInTableCell(
								result,
								value,
								i,
								val,
								row.IdStudent
							);
						});
					}); // each semester

					return result;
				},
			},
		],
		order: [[0, "desc"]],
	};
}

function getCreateSemesterUI(result, row) {
	var addBtnId = "open-semester-form-btn-" + row.IdStudent;
	var addSemesterFormId = "add-form-" + row.IdStudent;
	var addSemesterBtnId = "add-semester-btn-" + row.IdStudent;

	result = result + "<br/>";

	result =
		result +
		'<div class="row add-btn-wrapper" style="padding-left:10px;"><button class="col-5 btn btn-warning btn-sm" type="button" id="' +
		addBtnId +
		'">Add Semester</button></div>';
	result =
		result +
		'<form id="' +
		addSemesterFormId +
		'" style="display:none" >' +
		'<div class="form-group">' +
		"<label>Name</label>" +
		'<input type="text" class="form-control" name="name" placeholder="Semester name">' +
		"</div>" +
		'<div class="form-group">' +
		"<label>Start date</label>" +
		'<input type="text" class="form-control datepicker" name="start" placeholder="Chose date">' +
		"</div>" +
		'<div class="form-group">' +
		"<label>End date</label>" +
		'<input type="text" class="form-control datepicker" name="end" placeholder="Chose date">' +
		"</div>" +
		'<button type="button" id="' +
		addSemesterBtnId +
		'" class="btn btn-primary">Submit</button>' +
		"</form>";

	$(".datepicker").datepicker({
		format: "dd/mm/yyyy",
	});

	$("#" + addBtnId).on("click", function (e) {
		$("#" + addSemesterFormId).show();
	});
	$("#" + addSemesterBtnId).on("click", function (e) {
		var semesterName = $(
			"#" + addSemesterFormId + ' input[name="name"]'
		).val();
		var startDate = $(
			"#" + addSemesterFormId + ' input[name="start"]'
		).val();
		var endDate = $("#" + addSemesterFormId + ' input[name="end"]').val();
		postCreateSemester(row.IdStudent, semesterName, startDate, endDate);
	});

	return result;
}

function deleteSemester(semester, row) {
	if (semester.HasStudents) {
		alert("Only semesters without any students can be deleted!");
	} else {
		$.ajax({
			url: "/Semesters/Delete?id=" + semester.IdSemester,
			method: "DELETE",
			contentType: "application/json",
		})
			.done(function (res) {
				if (!!res.ErrorMessage) {
					alert(res.ErrorMessage);
				} else {
					row.remove().draw();
				}
			})
			.fail(function (err) {
				console.error(err.responseText);
				alert("Error deleting discipline!");
			});
	}
}

function postCreateStudent(firstName, lastName, dateBirth) {
	if (!firstName) {
		return alert("First name must not be empty!");
	}
	if (!lastName) {
		return alert("Last name must not be empty!");
	}
	if (!dateBirth) {
		return alert("Date of birth must have value!");
	}
	$.ajax({
		url:
			"/Students/Create?firstName=" +
			firstName +
			"&lastName=" +
			lastName +
			"&dateBirth=" +
			dateBirth,
		method: "POST",
		contentType: "application/json",
	})
		.done(function (res) {
			if (!!res.ErrorMessage) {
				alert(res.ErrorMessage);
			} else {
				location.reload(true);
			}
		})
		.fail(function (err) {
			console.error(err.responseText);
			alert("Error creating semester");
		});
}
