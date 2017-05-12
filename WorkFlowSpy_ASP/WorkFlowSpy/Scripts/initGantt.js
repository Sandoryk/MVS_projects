(function () {
   
    function setScaleConfig(value) {
        switch (value) {
            case "1":
                gantt.config.scale_unit = "day";
                gantt.config.step = 1;
                gantt.config.date_scale = "%d %M";
                gantt.config.subscales = [];
                gantt.config.scale_height = 27;
                gantt.templates.date_scale = null;
                break;
            case "2":
                var weekScaleTemplate = function (date) {
                    var dateToStr = gantt.date.date_to_str("%d %M");
                    var endDate = gantt.date.add(gantt.date.add(date, 1, "week"), -1, "day");
                    return dateToStr(date) + " - " + dateToStr(endDate);
                };

                gantt.config.scale_unit = "week";
                gantt.config.step = 1;
                gantt.templates.date_scale = weekScaleTemplate;
                gantt.config.subscales = [
					{ unit: "day", step: 1, date: "%D" }
                ];
                gantt.config.scale_height = 50;
                break;
            case "3":
                gantt.config.scale_unit = "month";
                gantt.config.date_scale = "%F, %Y";
                gantt.config.subscales = [
					{ unit: "day", step: 1, date: "%j, %D" }
                ];
                gantt.config.scale_height = 50;
                gantt.templates.date_scale = null;
                break;
            case "4":
                gantt.config.scale_unit = "year";
                gantt.config.step = 1;
                gantt.config.date_scale = "%Y";
                gantt.config.min_column_width = 50;

                gantt.config.scale_height = 50;
                gantt.templates.date_scale = null;


                gantt.config.subscales = [
					{ unit: "month", step: 1, date: "%M" }
                ];
                break;
        }
    }

    setScaleConfig('3');

    // configure milestone description
    gantt.templates.rightside_text = function (start, end, task) {
        if (task.type == gantt.config.types.milestone) {
            return task.text;
        }
        return "";
    };
    
    gantt.config.columns = [
		//{
		//    name: "overdue", label: "", width: 38, template: function (obj) {
		//        if (obj.deadline) {
		//            var deadline = gantt.date.parseDate(obj.deadline, "xml_date");
		//            if (deadline && obj.end_date > deadline) {
		//                return '<div class="overdue-indicator">!</div>';
		//            }
		//        }
		//        return '<div></div>';
		//    }
		//},
		{ name: "text", label: "Task name", width: "*", tree: true, resize: true },
		{ name: "start_date", label: "Start date", align: "center", width: 80 },
		{ name: "end_date", label: "End date", width: 80, align: "center" },
		{ name: "add", label: "", width: 36 }
    ];

    gantt.config.xml_date = "%Y-%m-%d %H:%i:%s"; // format of dates in XML
   
    var date_to_str = gantt.date.date_to_str(gantt.config.task_date);
    var markerId = gantt.addMarker({
        start_date: new Date(), //a Date object that sets the marker's date
        css: "today", //a CSS class applied to the marker
        text: "Now", //the marker title
        title: date_to_str(new Date()) // the marker's tooltip
    });

    var employees = [
    { key: 'Employee1', label: 'Employee1' },
    { key: 'Employee2', label: 'Employee2' },
    { key: 'Employee3', label: 'Employee3' }
    ];

    gantt.config.lightbox.sections = [
        { name: "description", height: 38, map_to: "text", type: "textarea", focus: true },
        { name: "type", height: 38, type: "typeselect", map_to: "type"},
        { name: "holder", height: 38, map_to: "holder", type: "select", options: employees },
        /*{ name: "time", height: 40, map_to: "auto", type: "duration" },*/
        { name:"period", height:40, map_to:"auto", type:"time"}
    ];
    gantt.locale.labels.section_holder = "Holder";
    gantt.locale.labels.section_period = "Time range";
    gantt.locale.labels.section_time = "Duration";

    //setInterval(function () {
    //    var today = gantt.getMarker(markerId);
    //    today.start_date = new Date();
    //    today.title = date_to_str(today.start_date);
    //    gantt.updateMarker(markerId);
    //}, 1000 * 60);

    gantt.attachEvent("onLightboxSave", function (id, item) {
        if (!item.text) {
            dhtmlx.message({ type: "error", text: "Enter task description!" });
            return false;
        }
        if (!item.type) {
            dhtmlx.message({ type: "error", text: "Choose type!" });
            return false;
        }
        if (!item.holder) {
            dhtmlx.message({ type: "error", text: "Choose a holder for this task!" });
            return false;
        }
        return true;
    });

    gantt.templates.progress_text = function (start, end, task) { //text in progress bar
        return /*"<span style='text-align:left;'>" +*/ Math.round(task.progress * 100) + "% </span>";
    };
    gantt.templates.task_text = function (start, end, task) {
        return /*"<span style='text-align:center;'>" +*/ task.text + "</span>";
    };

    gantt.config.order_branch = true;
    gantt.config.order_branch_free = true;
    gantt.config.sort = true;

    gantt.init("ganttContainer"); // initialize gantt

    var callScaleConfig = function (e) {
        e = e || window.event;
        var el = e.target || e.srcElement;
        var value = el.value;
        setScaleConfig(value);
        gantt.render();
    };

    var els = document.getElementsByName("scale");
    document.getElementById("scale3").setAttribute("checked", "1");
    for (var i = 0; i < els.length; i++) {
        els[i].onclick = callScaleConfig;
    }

    // enable dataProcessor
    var dp = new gantt.dataProcessor("/Home/SaveDiagramChanges");
    dp.init(gantt);
})();