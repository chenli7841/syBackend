;
orderDetails = (function () {
    let addBaggageUrl = '',
        addItemUrl = '',
        calculateItemCostUrl = '';

    function calculateTotalWeight() {
        let totalWeight = 0;
        $('.baggage-weight').each(function () { totalWeight += parseFloat($(this).val()); });
        $('#totalWeight').text(totalWeight);
    }

    function calculateTotalVolume() {
        let route = global_routes.find(r => r.id === parseInt($('#route-select').val()));
        let volumeWeightRatio = 0;
        if (route) {
            volumeWeightRatio = route.volumeWeightRatio;
        }
        const baggageRows = $('.row.baggage');
        let totalVolume = 0;
        for (let i = 0, il = baggageRows.length; i < il; i++) {
            let baggageSize = parseFloat($(baggageRows[i]).find('.baggage-length').val()) *
                parseFloat($(baggageRows[i]).find('.baggage-width').val()) *
                    parseFloat($(baggageRows[i]).find('.baggage-height').val());

            totalVolume += (baggageSize / (volumeWeightRatio || 5000));
        }

        $('#TotalVolume').val(totalVolume.toFixed(2));
    }

    function updateBaggageSummary() {
        calculateTotalWeight();
        calculateTotalVolume();
    }

    function calculateShippingCost() {
        let shippingCost = 0;
        $("#ItemCost, #Duty, #OversizeCost, #FumigationCost, #WarehouseCost, #PortMisCost, #StorageCost").each(
            function(index, element) {
                shippingCost += parseFloat($(element).val());
            });
        let rate = parseFloat($('#PickUpLocation_DistrictAdditionalCost').val());
        let weight = parseFloat($('#WeightKg').val());
        let districtAdditionalCost = rate*weight;
        shippingCost += districtAdditionalCost;
        shippingCost += parseFloat($('#InsuranceCost').val());
        shippingCost -= parseFloat($('#Discount').val());
        shippingCost = Math.round(shippingCost * 100) / 100

        $('#DistrictAdditionalCost').val(districtAdditionalCost);
        $('#ShippingCost').val(shippingCost);
    }

    function calculateItemCost() {
        calculateInsuranceFee();
        calculateTotalVolume();
        if (!$('#RouteId').val() || parseFloat($('#WeightKg').val()) === 0) {
            return;
        }
        var model = $('form').serialize();
        $.ajax({
            type: "POST",
            url: calculateItemCostUrl,
            data: model,
            success: function (result) {
                if (result.error) {
                    alert('Error!' + result.error);
                } else {
                    $('#ItemCost').val(Math.round(result.data * 100) / 100);
                    calculateShippingCost();
                }
            },
            error: function () {
                alert('Error!');
            }
        });
    }

    function onItemCategoryChange(categorySelect) {
        $(this).siblings('input').val($(this).val());
        calculateItemCost();
    }

    function onItemMaterialChange() {
        $(this).siblings('input').val($(this).val());
    }

    function bindItemClickEvents() {
        $('i.delete-item').off().click(function () {
            const itemRow = $(this).closest('div.row.item');
            itemRow.find('.action-input').val('Delete');
            itemRow.hide();
        });

        $('i.add-item').off().click(function () {
            const totalItems = $('#items-listing div.item').length;
            $.post(addItemUrl,
                { index: totalItems },
                function (html) {
                    $('#items-listing').append(html);
                    bindItemClickEvents();
                });
        });

        $('.item-category-select').off("change", onItemCategoryChange).change(onItemCategoryChange);

        $('.item-material-select').off("change", onItemMaterialChange).change(onItemMaterialChange);
    }

    function bindBaggageClickEvents() {
        $('i.delete-baggage').off().click(function () {
            const itemRow = $(this).closest('div.row.baggage');
            itemRow.find('.action-input').val('Delete');
            itemRow.hide();
            itemRow.siblings('p').hide();
        });

        $('i.add-baggage').off().click(function () {
            const totalBaggages = $('#baggage-listing div.baggage').length;
            $.post(addBaggageUrl,
                { index: totalBaggages },
                function (html) {
                    $('#baggage-listing').append(html);
                    $('#totalBaggage').text(totalBaggages + 1);
                    bindBaggageClickEvents();
                });
        });

        $('.row.baggage input').off("change", updateBaggageSummary).change(updateBaggageSummary);
    }

    function calculateInsuranceFee() {
        let route = global_routes.find(r => r.id === parseInt($('#route-select').val()));
        let insuranceRatio = 0;
        if (route) {
            insuranceRatio = route.insuranceRatio;
        }
        let insuranceAmount = (parseFloat($("#Insurance").val()) || 0);
        $("#InsuranceCost").val(Math.round(insuranceAmount * insuranceRatio * 100) / 100);
    }


    function init(options) {
        addBaggageUrl = options.addBaggageUrl;
        addItemUrl = options.addItemUrl;
        calculateItemCostUrl = options.calculateItemCostUrl;

        bindItemClickEvents();
        bindBaggageClickEvents();

        $('#update_cost').click(function(e) {
            calculateItemCost();

            e.preventDefault();
            return false;
        });

        $('#route-select').val($('#RouteId').val());

        $('#route-select').change(function () {
            $('#RouteId').val(this.value);
            calculateItemCost();
        });

        $('#domesticCarrier-select').val($('#DomesticCarrier').val());

        $('#domesticCarrier-select').change(function () {
            $('#DomesticCarrier').val(this.value);
        });

        const itemPropSelects = $('.item-material-select, .item-category-select');
        for (let i = 0, il = itemPropSelects.length; i < il; i++) {
            const ms = $(itemPropSelects[i]);
            ms.val(ms.siblings('input').val());
        }

        $('#Insurance').change(calculateInsuranceFee);

        $("#ItemCost, #Duty, #OversizeCost, #FumigationCost, #WarehouseCost, #PortMisCost, #StorageCost, #Insurance, #Discount")
            .change(calculateShippingCost);

        $('#WeightKg').change(calculateItemCost);
    }

    return {
        init
    }
})();