import { useState, useEffect } from 'react';
import axios from 'axios';
import Auth from './components/Auth';
import TaskList from './components/TaskList';
import TaskDetail from './components/TaskDetail';
import TaskForm from './components/TaskForm';

const TASKS_API = "https://localhost:7233/api/Tasks";
const AUTH_API = "https://localhost:7233/api/Auth";

function App() {
  const [user, setUser] = useState(null);
  const [tasks, setTasks] = useState([]);
  const [isRegistering, setIsRegistering] = useState(false);
  const [authData, setAuthData] = useState({ username: "", password: "" });
  const [searchTerm, setSearchTerm] = useState("");

  const [selectedTask, setSelectedTask] = useState(null);
  const [editingTask, setEditingTask] = useState(null);
  const [isAddingTask, setIsAddingTask] = useState(false);

  // Yeni görev ekleme için geçici state
  const [tempTask, setTempTask] = useState({ title: "", content: "", state: 0 });
  // App.js içindeki diğer state'lerin yanına ekle
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    if (user?.userId) fetchTasks();
    else setTasks([]);
  }, [user]);

  // --- API FONKSİYONLARI ---

  const fetchTasks = async () => {
    try {
      const response = await axios.get(`${TASKS_API}/user/${user.userId}`);
      setTasks(response.data);
    } catch (err) { console.error("Veri çekilemedi:", err); }
  };

  const addTask = async () => {
    if (!tempTask.title || !tempTask.content) return alert("Lütfen tüm alanları doldurun!");
    try {
      await axios.post(TASKS_API, {
        Title: tempTask.title,
        Content: tempTask.content,
        UserId: user.userId
      });
      setTempTask({ title: "", content: "", state: 0 });
      setIsAddingTask(false);
      fetchTasks();
    } catch (err) { alert("Görev eklenemedi!"); }
  };

  const handleUpdate = async () => {
    try {
      await axios.put(`${TASKS_API}/${editingTask.id}`, {
        id: editingTask.id,
        title: editingTask.title,
        // Backend DTO'nda 'content' beklediği için 'content' ismini kullanıyoruz
        content: editingTask.content || editingTask.description,
        state: parseInt(editingTask.state)
      });
      setEditingTask(null);
      fetchTasks();
    } catch (error) { alert("Güncelleme hatası!"); }
  };

  const deleteTask = async (id) => {
    if (window.confirm("Bu görevi silmek istediğinize emin misiniz?")) {
      try {
        await axios.delete(`${TASKS_API}/${id}`);
        fetchTasks();
      } catch (err) { alert("Silme hatası!"); }
    }
  };

  const handleLogout = () => {
    setUser(null);
    setTasks([]);
    setSearchTerm("");
  };

  // --- RENDERING LOGIC ---

  if (!user) return (
    <Auth
      isLoading={isLoading}
      isRegistering={isRegistering}
      setIsRegistering={setIsRegistering}
      authData={authData}
      setAuthData={setAuthData}
      handleLogin={async () => {
        if (isLoading) return
        try {
          const res = await axios.post(`${AUTH_API}/login`, authData);
          setUser(res.data);
        } catch { alert("Giriş başarısız!"); }
        finally {
          setIsLoading(false);
        }
      }}
      handleRegister={async () => {
        try {
          await axios.post(`${AUTH_API}/register`, authData);
          alert("Kayıt başarılı!");
          setIsRegistering(false);
        } catch { alert("Kayıt hatası!"); }
      }}
    />
  );

  if (selectedTask) return <TaskDetail task={selectedTask} onBack={() => setSelectedTask(null)} />;

  if (isAddingTask || editingTask) return (
    <TaskForm
      mode={editingTask ? 'edit' : 'add'}
      taskData={editingTask || tempTask}
      setTaskData={editingTask ? setEditingTask : setTempTask}
      onSave={editingTask ? handleUpdate : addTask}
      onCancel={() => { setEditingTask(null); setIsAddingTask(false); }}
    />
  );

  return (
    <TaskList
      user={user}
      tasks={tasks}
      searchTerm={searchTerm}
      setSearchTerm={setSearchTerm}
      onLogout={handleLogout}
      onAddTaskClick={() => setIsAddingTask(true)}
      onTaskClick={setSelectedTask}
      onEditClick={setEditingTask}
      onDeleteClick={deleteTask}
    />
  );
}

export default App;