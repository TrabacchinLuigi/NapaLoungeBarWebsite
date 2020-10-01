window.NapaJsInterop = {
    DotNetInstance: {},
    Initialize: function (dotNetInstance) {
        console.info("initilized js Interop");
        window.NapaJsInterop.DotNetInstance = dotNetInstance;
    },
    OnAfterRender: function (isFirstRender) {
        console.info("called after render");

        if (isFirstRender) {
            let mainContainer = document.getElementById("main-container");
            mainContainer.classList.remove("container");
            mainContainer.classList.add("container-lg");

            let element = document.getElementById("delete-entity-modal");
            let jqElement = $(element)
            jqElement.modal({ show: false });
            jqElement.on('hidden.bs.modal', function (e) {
                console.info("invoking HandleClosedDeleteModal");
                window.NapaJsInterop.DotNetInstance.invokeMethodAsync('HandleClosedDeleteModal')
                    .then(function () {
                        console.info("completed HandleClosedDeleteModal");
                    });
            });

            let deleteButtonElement = document.getElementById('delete-confirm');
            deleteButtonElement.addEventListener("click", function (ev) {
                console.info("invoking ConfirmDelete");
                window.NapaJsInterop.DotNetInstance.invokeMethodAsync('ConfirmDelete')
                    .then(function () {
                        console.info("completed ConfirmDelete");
                        jqElement.modal('hide');
                    });
            });
        }
        // add popper js
        $('[data-toggle="tooltip"]').tooltip('dispose');
        $('[data-toggle="tooltip"]').tooltip();
    },
    ShowDeleteModal: function (what, kind) {
        console.info("running ShowDeleteModal")
        let whatElement = $('#delete-what-name');
        whatElement.text(what);
        let kindElement = $('#delete-what-kind');
        kindElement.text(kind);

        let element = $('#delete-entity-modal');
        element.modal('show');
    }
};