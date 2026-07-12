// 批次详情页"检查重复运单"功能：扫描当前批次所有箱子，找出同一运单出现在多个箱子里的情况，
// 列出每个重复运单出现的所有箱号，每个位置后面跟一个"移除"按钮，可以单独把该运单从某个箱子里移除。
(function () {
    function renderDuplicates(duplicates) {
        const $tbody = $('#duplicate-orders-tbody');
        const $result = $('#duplicate-orders-result');
        $tbody.empty();

        if (!duplicates || duplicates.length === 0) {
            $tbody.append('<tr><td colspan="4">未发现重复运单</td></tr>');
            $result.show();
            return;
        }

        duplicates.forEach(function (dup) {
            dup.locations.forEach(function (loc) {
                const row = $(`<tr>
                    <td>${dup.orderNumber || ''}</td>
                    <td>${dup.domesticNumber || ''}</td>
                    <td>箱号 ${loc.boxNumber}</td>
                    <td><button class="btn btn-sm btn-danger remove-duplicate-order-btn" data-box-id="${loc.boxId}" data-order-id="${dup.orderId}">移除</button></td>
                </tr>`);
                $tbody.append(row);
            });
        });
        $result.show();
    }

    function checkDuplicates(batchId) {
        $.get('/Batch/CheckDuplicateOrders', { id: batchId }, function (result) {
            if (result.error) {
                $.toast({
                    heading: result.error.text,
                    position: 'mid-center',
                    icon: 'error'
                });
                return;
            }
            renderDuplicates(result.data);
        });
    }

    $(document).on('click', '#check-duplicate-orders-btn', function () {
        checkDuplicates($(this).data('batch-id'));
    });

    $(document).on('click', '.remove-duplicate-order-btn', function () {
        const $btn = $(this);
        const boxId = $btn.data('box-id');
        const orderId = $btn.data('order-id');
        const batchId = $('#check-duplicate-orders-btn').data('batch-id');

        if (!confirm('确认把这条运单从该箱子移除吗？')) {
            return;
        }

        $.post('/Batch/RemoveDuplicateOrder', { boxId, orderId }, function (result) {
            if (result.error) {
                $.toast({
                    heading: result.error.text,
                    position: 'mid-center',
                    icon: 'error'
                });
                return;
            }
            $.toast({
                heading: '成功',
                text: '已移除',
                showHideTransition: 'slide',
                icon: 'success',
                loaderBg: '#f2a654',
                position: 'mid-center'
            });
            checkDuplicates(batchId);
        });
    });
})();
