import React, {useEffect, useMemo} from 'react';
import {useNavigate} from "react-router-dom";
import {
  Calendar,
  dateFnsLocalizer,
} from 'react-big-calendar';
import 'react-big-calendar/lib/css/react-big-calendar.css';
import {Grid} from "@mui/material";
import {
  format,
  getDay,
  parse,
  startOfWeek,
  addMinutes
} from 'date-fns';
import {ru} from "date-fns/locale";
import {useDispatch} from "react-redux";
import {getAllSubjects, getSheduler} from "../store/subjectStore/asyncActions";
import {useTypedSelector} from "../hooks/useTypedSelector";
import {Loading} from "../layout/Loading";
import {subjectActions} from "../store/subjectStore";

const locales = {
  ru: ru,
};

const localizer = dateFnsLocalizer({
  format,
  parse,
  startOfWeek,
  getDay,
  locales,
});

export const CalendarPage: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const {
    sheduler,
    subjects,
    loadingInitial
  } = useTypedSelector(s => s.subject);
  const events = useMemo(() => {
    const result = subjects.map((subject => ({
      title: <>
        <div
          onClick={async () => await dispatch(getSheduler(subject.name))}
        >{subject.name} {subject.type} {subject.time.slice(0, -3)}</div>
      </>,
      startDate: parse(
        `${subject.date} ${subject.time.slice(0, -3)}`,
        'dd.MM.yyyy H:mm', new Date()
      ),
      endDate: addMinutes(parse(
        `${subject.date} ${subject.time.slice(0, -3)}`,
        'dd.MM.yyyy H:mm', new Date()
      ), 90)
    })));

    return result;
  }, [dispatch, subjects]);

  useEffect(() => {
    if (sheduler) {
      navigate(`/attendances/${sheduler.id}`);
      dispatch(subjectActions.setSheduler(null));
    }
  }, [dispatch, navigate, sheduler]);

  useEffect(() => {
    dispatch(getAllSubjects());
  }, [dispatch]);

  if (loadingInitial) {
    return <Loading loading={loadingInitial}/>;
  }
  return (
    <Grid container style={{height: '90%', padding: '.3rem'}}>
      <Calendar
        localizer={localizer}
        startAccessor='startDate'
        endAccessor='endDate'
        titleAccessor='title'
        views={['month', 'week', 'day', 'agenda']}
        culture='ru'
        messages={{
          next: 'Следующая',
          previous: 'Предыдущая',
          today: 'Сегодня',
          month: 'Месяц',
          week: 'Неделя',
          work_week: 'Рабочая неделя',
          day: 'День',
          agenda: 'Ежемесечная повестка',
          date: 'Дата',
          time: 'Время',
          event: 'Событие',
          noEventsInRange: 'Нет занятий на текущий месяц'
        }}

        events={events}
        style={{flexGrow: 1}}
      />
    </Grid>
  );
};
