import React, {useCallback} from 'react';
import {Grid, useTheme, TextField} from "@mui/material";
import {
  DatePicker,
  LocalizationProvider,
  PickersDay,
  PickersDayProps, pickersDayClasses
} from "@mui/lab";
import AdapterDateFns from "@mui/lab/AdapterDateFns";
import {ru} from "date-fns/locale";
import {format, isSameDay} from "date-fns";

type PropsType = {
  id: string;
  dates: Date[];
  error: boolean;
  setDates: (dates: Date[]) => void;
};

export const MultipleDatePicker: React.FC<PropsType> = ({
                                                          id,
                                                          dates,
                                                          setDates,
                                                          error
                                                        }) => {
  const theme = useTheme();

  const renderSelecetedDays = useCallback((
    date: Date,
    selectedDates: Array<Date | null>,
    pickersDayProps: PickersDayProps<Date>
  ) => {
    const curDate = format(date, 'dd.MM.yyyy');
    const findDate = dates
      .find(d => format(d, 'dd.MM.yyyy') === curDate);
    const matchedStyles = dates.reduce((a, v) => {
      return isSameDay(date, v) ? {
        background: theme.palette.primary.main
      } : a;
    }, {});

    return (
      <PickersDay
        {...pickersDayProps}
        sx={{
          ...matchedStyles,
          [`&&.${!findDate && pickersDayClasses.selected}`]: {
            backgroundColor: theme.palette.primary.contrastText,
            color: theme.palette.text.primary
          }
        }}
      />
    );
  }, [theme, dates]);

  return (
    <Grid container spacing={1}>
      <Grid item>
        <LocalizationProvider
          dateAdapter={AdapterDateFns}
          locale={ru}
        >
          <DatePicker
            mask='__.__.____'
            disableCloseOnSelect
            allowSameDateSelection
            views={['day']}
            label='Даты проведения'
            value={dates.length > 0 ? dates[dates.length - 1] : null}
            onChange={(newDate) => {
              const curDate = format(newDate!, 'dd.MM.yyyy');
              const findDate = dates
                .find(d => format(d, 'dd.MM.yyyy') === curDate);

              if (newDate && !findDate) {
                setDates([
                  ...dates,
                  newDate
                ]);
              } else if (newDate) {
                setDates(dates.filter(d => d !== findDate));
              }
            }}
            renderDay={renderSelecetedDays}
            renderInput={(params) =>
              <TextField
                id={id}
                {...params}
                fullWidth
                error={error}
              />}
          />
        </LocalizationProvider>
      </Grid>
    </Grid>
  );
};
