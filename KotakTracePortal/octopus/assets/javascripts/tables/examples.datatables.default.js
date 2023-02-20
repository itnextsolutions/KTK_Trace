

(function( $ ) {

	'use strict';

	var datatableInit = function() {
	    $('#datatable-default').dataTable();
	    $('#datatable-Second').dataTable();
	    $('#datatable-Third').dataTable();
	    $('#datatable-Four').dataTable();
        
	    //$('#datatable-default').dataTable({
	    //    destroy: true,
	    //    dom: "Bfrtlip",
	    //    "oLanguage": { "sSearch": "" },
	    //    buttons: [{
	    //        extend: 'excel',
	    //        text: ' <button type="button" class="btn btn-success pull-right" style="margin-right:15px;"><i class="fa fa-file-excel-o"></i> Export to Excel</button>',
	    //        //title: 'Item Master List',
	    //        //exportOptions: {
	    //        //    modifier: {
	    //        //        page: 'all',
	    //        //        order: 'index'
	    //        //    },
	    //        //    columns: [0, 1, 2, 3, 4, 5, 6, 7]
	    //        //}
	    //    }]
	    //});

	};

	$(function() {
		datatableInit();
	});

}).apply( this, [ jQuery ]);