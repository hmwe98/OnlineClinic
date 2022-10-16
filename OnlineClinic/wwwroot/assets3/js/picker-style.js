        $('#date1').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate1',
            targetDateSelector: '#inputDate1-1',
            dateFormat: 'yyyy-MM-dd',
            isGregorian: false,
            enableTimePicker: true,
            disableBeforeToday: true,
            disabledDates: [new Date(), new Date(2018, 9, 11), new Date(2018, 9, 12), new Date(2018, 9, 13),
                new Date(2018, 9, 14)
            ],
            disabledDays: [5, 6]
        });

        $('#fromDateSegment').MdPersianDateTimePicker({
            disableAfterDate: new Date(),
            targetTextSelector: '#inputFromDate',
            targetDateSelector: '#fromdate',
            isGregorian: false,
            enableTimePicker: false,
            englishNumber: false
        });

        $('#date2').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate2',
            selectedDate: new Date('2018/9/30'),
            isGregorian: true,
            calendarViewOnChange: function (date) {
                console.log(date);
            }
        });

        $('#date3').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate3',
            monthsToShow: [1, 1],
        });
        $('#date3-1').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate3-1',
            monthsToShow: [1, 1],
            rangeSelector: true
        });

        $('#date4').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate4',
            fromDate: true,
            enableTimePicker: true,
            groupId: 'rangeSelector1',
            dateFormat: 'yyyy-MM-dd HH:mm:ss',
            textFormat: 'yyyy-MM-dd',
        });
        $('#date5').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate5',
            toDate: true,
            groupId: 'rangeSelector1',
            placement: 'top',
            enableTimePicker: true,
            dateFormat: 'yyyy-MM-dd HH:mm:ss',
            textFormat: 'yyyy-MM-dd',
        });

        $('#date7').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate8',
            targetDateSelector: '#inputDate9',
            inLine: true,
            enableTimePicker: true,
            toDate: true,
            groupId: 'date6-7-range'
        });

        $('#date6').MdPersianDateTimePicker({
            targetTextSelector: '#inputDate6',
            targetDateSelector: '#inputDate7',
            inLine: true,
            enableTimePicker: true,
            fromDate: true,
            groupId: 'date6-7-range'
        });