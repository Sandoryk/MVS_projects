(function () {
    // add month scale
    gantt.config.scale_unit = "month";
	gantt.config.step = 1;
	gantt.config.date_scale = "%F, %Y";

	gantt.config.scale_height = 50;

	var weekScaleTemplate = function(date){
		var dateToStr = gantt.date.date_to_str("%d %M");
		var endDate = gantt.date.add(gantt.date.add(date, 1, "week"), -1, "day");
		return dateToStr(date) + " - " + dateToStr(endDate);
	};

	gantt.config.subscales = [
		{unit:"week", step:1, template:weekScaleTemplate},
		{unit:"day", step:1, date:"%D" }
	];

    // configure milestone description
    gantt.templates.rightside_text = function (start, end, task) {
        if (task.type == gantt.config.types.milestone) {
            return task.text;
        }
        return "";
    };
    // add section to type selection: task, project or milestone
    gantt.config.lightbox.sections = [
        { name: "description", height: 70, map_to: "text", type: "textarea", focus: true },
        { name: "type", type: "typeselect", map_to: "type" },
        { name: "time", height: 72, type: "duration", map_to: "auto" }
    ];

    gantt.config.xml_date = "%Y-%m-%d %H:%i:%s"; // format of dates in XML
    gantt.templates.progress_text = function (start, end, task) { //text in progress bar
        return "<span style='text-align:left;'>" + Math.round(task.progress * 100) + "% </span>";
    };
    gantt.config.order_branch = true;
    gantt.config.order_branch_free = true;
    gantt.config.sort = true;

    var date_to_str = gantt.date.date_to_str(gantt.config.task_date);
    var markerId = gantt.addMarker({
        start_date: new Date(), //a Date object that sets the marker's date
        css: "today", //a CSS class applied to the marker
        text: "Now", //the marker title
        title: date_to_str(new Date()) // the marker's tooltip
    });

    gantt.config.lightbox.sections = [
    { name: "description", height: 38, map_to: "text", type: "textarea", focus: true },
    { name: "type", type: "typeselect", map_to: "type" },
    { name: "holder", height: 38, map_to: "assignedTo", type: "textarea" },
    { name: "time", height: 72, map_to: "auto", type: "duration" }
    ];
    gantt.locale.labels.section_holder = "Holder";
    //setInterval(function () {
    //    var today = gantt.getMarker(markerId);
    //    today.start_date = new Date();
    //    today.title = date_to_str(today.start_date);
    //    gantt.updateMarker(markerId);
    //}, 1000 * 60);

    gantt.init("ganttContainer"); // initialize gantt

    // enable dataProcessor
    var dp = new gantt.dataProcessor("/Home/SaveDiagramChanges");
    dp.init(gantt);
})();