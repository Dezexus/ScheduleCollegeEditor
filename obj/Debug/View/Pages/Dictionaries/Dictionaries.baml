   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5      Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  5      -    'ScheduleCollegeEditor.View.Dictionaries   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:ScheduleCollegeEditor.View     5      C	viewmodel-clr-namespace:ScheduleCollegeEditor.ViewModel     5      $��Stretch=�5	      $��Stretch=�5
        � DataContext  5      ;  5ScheduleCollegeEditor.ViewModel.DictionariesViewModel  5   
   .��5      ��  �� Orientation$ 
Horizontal=�6   $��35��6*   $��Center=�66   $��0,10,0,0q�6S   .+�5   
   �� $ ��Преподаватели��6   $	��125��6)      
ButtonBase#���   65   ��6Z   �� *OpenTeachersPageCommand+�� 5   
   $��Группы��6   $	��125��6"   $	��5,0q�6.   #���   6;   ��6`   �� *OpenGroupsPageCommand+�� 5   
   $��Предметы��6   $	��125��6$   #���   60   ��6U   �� *OpenSchoolSubjectsPageCommand+�� 5   
   $#��Предметы группы��6   $	��125��6+   $	��5,0q�67   #���   6D   ��6i   �� *$"OpenGroupSchoolSubjectsPageCommand+�� 5   
   $��Кабинеты��6   $	��125��6$   #���   60   ��6U   �� *OpenCabinetsPageCommand+�� 5   
   $��
Время��6   $	��125��6!   $��5,0,0,0q�6-   #���   6>   ��6c   �� *OpenTimesPageCommand+5      5      