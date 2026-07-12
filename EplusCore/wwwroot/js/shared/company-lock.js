// 当后台设置了"锁定公司"后，页面上所有公司选择下拉菜单（id="company-select" 或 id="CompanyId"）
// 强制显示/选中该公司，并禁用下拉菜单本身的交互功能。
// 用 window load（而非 document ready）确保在页面自身的初始化脚本（例如根据 URL 参数回填下拉值）之后执行，
// 避免被页面脚本重新赋值或覆盖。
(function () {
    function lockCompanySelects() {
        var lockedCompanyId = window.__lockedCompanyId;
        if (lockedCompanyId === undefined || lockedCompanyId === null) {
            return;
        }
        var selects = document.querySelectorAll('#company-select, #CompanyId');
        selects.forEach(function (select) {
            if (select.tagName !== 'SELECT') {
                return;
            }
            select.value = String(lockedCompanyId);
            // 如果锁定的公司不在当前选项列表里（极端情况，比如公司被删除），不要强行选中一个不存在的值
            if (select.value !== String(lockedCompanyId)) {
                return;
            }
            select.disabled = true;
            // 若此下拉本身承担表单提交（有 name 属性），disabled 的表单控件不会随表单提交，
            // 需要补一个同名隐藏字段，确保提交值不丢失。
            var name = select.getAttribute('name');
            if (name && !select.dataset.lockShadowAdded) {
                var hidden = document.createElement('input');
                hidden.type = 'hidden';
                hidden.name = name;
                hidden.value = String(lockedCompanyId);
                select.insertAdjacentElement('afterend', hidden);
                select.dataset.lockShadowAdded = 'true';
            }
        });
    }

    if (document.readyState === 'complete') {
        lockCompanySelects();
    } else {
        window.addEventListener('load', lockCompanySelects);
    }
})();
