let alertPlaceholder = document.getElementById('liveAlertPlaceholder');
const notify = (response) => {
    $(document).ready(() => {
        let wrapper = document.createElement('div');
        wrapper.classList.add('notification-wrapper')
        if (response.Success) {
            wrapper.innerHTML = `<div class="alert alert-success d-flex align-items-center fade show alert-dismissible" role="alert" id="alert">
            <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Success:"><use xlink:href="#check-circle-fill"/></svg>
            <div class="text-center mx-auto">
                ${response}
            </div>
            <button id="close-alert" type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>`
        } else {
            wrapper.innerHTML = `<div class="alert alert-danger d-flex align-items-center fade show alert-dismissible" role="alert" id="alert">
                <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Danger:"><use xlink:href="#exclamation-triangle-fill"/></svg>
                <div class="text-center mx-auto">
                ${response}
                </div>
                <button id="close-alert" type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>`
        }
        alertPlaceholder.append(wrapper)
        setTimeout(() => {
            $('#close-alert').click();
        }, 5000);
    });
}