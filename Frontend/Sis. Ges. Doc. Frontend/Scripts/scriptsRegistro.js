// Minimal client-side upload manager:
// - initFileUpload registers the selected file but does not upload it.
// - uploadPendingFile uploads the stored file and returns a Promise.
var _pendingFiles = {};

function initFileUpload(fileInputId, statusId, uploadUrl) {
    try {
        var fileInput = document.getElementById(fileInputId);
        var status = document.getElementById(statusId);
        if (!fileInput) return;

        fileInput.addEventListener('change', function () {
            if (!this.files || this.files.length === 0) {
                delete _pendingFiles[fileInputId];
                if (status) status.textContent = '';
                // also update registrar state if validation is wired
                try { if (window._updateRegistrarState) window._updateRegistrarState(); } catch (_) {}
                return;
            }
            _pendingFiles[fileInputId] = this.files[0];
            if (status) {
                status.style.color = '#444';
                status.textContent = 'Archivo listo: ' + this.files[0].name;
            }
            try { if (window._updateRegistrarState) window._updateRegistrarState(); } catch (_) {}
        });
    } catch (e) {
        try { console.error(e); } catch (_) { }
    }
}

function uploadPendingFile(fileInputId, statusId, uploadUrl) {
    return new Promise(function (resolve, reject) {
        var f = _pendingFiles[fileInputId];
        var status = document.getElementById(statusId);
        if (!f) { resolve(); return; }

        var formData = new FormData();
        formData.append('file', f, f.name);

        if (status) { status.style.color = '#444'; status.textContent = 'Subiendo "' + f.name + '"...'; }

        fetch(uploadUrl, {
            method: 'POST',
            body: formData,
            credentials: 'same-origin'
        }).then(function (resp) {
            if (!resp.ok) throw new Error('HTTP ' + resp.status);
            return resp.json().catch(function () { return resp.text(); });
        }).then(function (data) {
            if (status) {
                status.style.color = '#2b7a2b';
                status.textContent = (data && data.mensaje) ? data.mensaje : 'Carga completada.';
            }
            // remember last uploaded filename for possible server-side use
            try { window._lastUploadedFile = f.name; } catch (_) { }
            delete _pendingFiles[fileInputId];
            try { if (window._updateRegistrarState) window._updateRegistrarState(); } catch (_) {}
            resolve(data);
        }).catch(function (err) {
            if (status) { status.style.color = '#a00'; status.textContent = 'Error al subir el archivo.' + (err && err.message ? ' ' + err.message : ''); }
            reject(err);
        });
    });
}

function hasPendingFile(fileInputId) {
    return !!_pendingFiles[fileInputId];
}

// expose to global scope
window.initFileUpload = initFileUpload;
window.uploadPendingFile = uploadPendingFile;
window.hasPendingFile = hasPendingFile;

// Convenience initializer: wires file input and registrar button.
// fileInputId: client id of file input
// statusId: id of status element
// uploadUrl: URL to post file to
// btnUniqueID: ASP.NET UniqueID for __doPostBack
// btnClientID: client id of the registrar button
function setupUpload(fileInputId, statusId, uploadUrl, btnUniqueID, btnClientID) {
    initFileUpload(fileInputId, statusId, uploadUrl);

    // bind click handler to registrar button
    try {
        var btn = document.getElementById(btnClientID);
        if (!btn) return;

        btn.addEventListener('click', function (e) {
            // If there's a pending file, prevent default/postback and upload first
            console.log("listener click");
            if (hasPendingFile(fileInputId)) {
                e.preventDefault();
                uploadPendingFile(fileInputId, statusId, uploadUrl).then(function () {
                    // after upload, trigger ASP.NET postback
                    try { __doPostBack(btnUniqueID, ''); } catch (ex) {
                        console.log("calling __doPostBack");
                        var form = document.getElementById('form1');
                        if (form) form.submit();
                    }
                }).catch(function () {
                    // upload error handled in uploadPendingFile; keep form from submitting
                });
            }
            // else allow normal postback
        });
    } catch (ex) {
        try { console.error(ex); } catch (_) { }
    }
}

// Validation helpers
function _fieldHasValue(el) {
    if (!el) return false;
    if (el.type === 'file') {
        return (el.files && el.files.length > 0) || (window._lastUploadedFile && window._lastUploadedFile.length > 0);
    }
    var v = el.value;
    if (typeof v === 'string') return v.trim().length > 0;
    return !!v;
}

// updateRegistrarState is kept in a global variable so setupUpload/initFileUpload can trigger it
window._updateRegistrarState = function () {
    try {
        if (!window._validationConfig) return;
        var cfg = window._validationConfig;
        var fileEl = document.getElementById(cfg.fileInputId);
        var nameEl = document.getElementById(cfg.nameId);
        var descEl = document.getElementById(cfg.descId);
        var catEl = document.getElementById(cfg.catId);
        var dateEl = document.getElementById(cfg.dateId);
        var btn = document.getElementById(cfg.btnId);
        if (!btn) return;

        var ok = _fieldHasValue(fileEl) && _fieldHasValue(nameEl) && _fieldHasValue(descEl) && _fieldHasValue(catEl) && _fieldHasValue(dateEl);
        btn.disabled = !ok;
        btn.style.opacity = ok ? '' : '0.6';
    } catch (e) { try { console.error(e); } catch (_) {} }
};

function setupValidation(fileInputId, nameId, descId, catId, dateId, btnClientID, statusId) {
    // store config for shared updater
    window._validationConfig = {
        fileInputId: fileInputId,
        nameId: nameId,
        descId: descId,
        catId: catId,
        dateId: dateId,
        btnId: btnClientID,
        statusId: statusId
    };

    try {
        var fileEl = document.getElementById(fileInputId);
        var nameEl = document.getElementById(nameId);
        var descEl = document.getElementById(descId);
        var catEl = document.getElementById(catId);
        var dateEl = document.getElementById(dateId);
        var btn = document.getElementById(btnClientID);
        var status = document.getElementById(statusId);
        if (!btn) return;

        var update = window._updateRegistrarState;

        if (fileEl) fileEl.addEventListener('change', update);
        if (nameEl) nameEl.addEventListener('input', update);
        if (descEl) descEl.addEventListener('input', update);
        if (catEl) catEl.addEventListener('input', update);
        if (dateEl) dateEl.addEventListener('change', update);

        // initial state
        update();

    } catch (e) { try { console.error(e); } catch (_) {} }
}

function onRegistrarClick() {
    console.log("client click");
    try {
        if (!window._validationConfig) return true; // no validation wired
        var cfg = window._validationConfig;
        var fileEl = document.getElementById(cfg.fileInputId);
        var nameEl = document.getElementById(cfg.nameId);
        var descEl = document.getElementById(cfg.descId);
        var catEl = document.getElementById(cfg.catId);
        var dateEl = document.getElementById(cfg.dateId);

        var ok = _fieldHasValue(fileEl) && _fieldHasValue(nameEl) && _fieldHasValue(descEl) && _fieldHasValue(catEl) && _fieldHasValue(dateEl);
        if (!ok) {
            // focus first invalid
            if (! _fieldHasValue(nameEl)) { nameEl && nameEl.focus(); }
            else if (! _fieldHasValue(descEl)) { descEl && descEl.focus(); }
            else if (! _fieldHasValue(catEl)) { catEl && catEl.focus(); }
            else if (! _fieldHasValue(dateEl)) { dateEl && dateEl.focus(); }
            else if (!_fieldHasValue(fileEl)) { fileEl && fileEl.focus(); }
            console.log("validación Neles")
            return false;
        }
        console.log("validación OK")
        return true;
    } catch (e) { try { console.error(e); } catch (_) {} return false; }
}

window.setupUpload = setupUpload;
window.setupValidation = setupValidation;
window.onRegistrarClick = onRegistrarClick;
