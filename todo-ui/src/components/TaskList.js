import React from 'react';

const RenderState = ({ state }) => {
  const config = {
    0: { text: "Yapılacak", color: "#f56565" },
    1: { text: "Yapılıyor", color: "#ed8936" },
    2: { text: "Bitti", color: "#48bb78" }
  };
  const current = config[state] || config[0];
  return (
    <span style={{
      backgroundColor: current.color, color: 'white', padding: '4px 10px',
      borderRadius: '20px', fontSize: '11px', fontWeight: 'bold'
    }}>{current.text}</span>
  );
};

export default function TaskList({
  user, tasks, searchTerm, setSearchTerm, onLogout,
  onAddTaskClick, onTaskClick, onEditClick, onDeleteClick
}) {
  return (
    <div className="container">
      <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '40px' }}>
        <div>
          <h1>My Todo App</h1>
          <p>Hoş geldin, <strong>{user.username}</strong></p>
        </div>
        <div style={{ display: 'flex', gap: '10px' }}>
          <button className="add-btn" onClick={onAddTaskClick}>+ Yeni Görev</button>
          <button onClick={onLogout} style={{ backgroundColor: '#718096', color: 'white', borderRadius: '8px', border: 'none', padding: '0 15px', cursor: 'pointer' }}>Çıkış Yap</button>
        </div>
      </header>

      <div style={{ marginBottom: '20px' }}>
        <input
          type="text"
          placeholder="Görev adı veya içeriğinde ara..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="search-input"
          style={{ width: '100%', padding: '12px', borderRadius: '8px', border: '1px solid #ddd' }}
        />
      </div>

      <div style={{ display: 'flex', gap: '20px' }}>
        {[0, 1, 2].map(stateVal => (
          <div key={stateVal} className="column" style={{ flex: 1 }}>
            <h2 style={{ borderBottom: '2px solid #eee', paddingBottom: '10px' }}>
              {stateVal === 0 ? "Yapılacak" : stateVal === 1 ? "Yapılıyor" : "Bitti"}
            </h2>
            {tasks
              .filter(t => t.state === stateVal)
              .filter(t =>
                t.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                (t.content && t.content.toLowerCase().includes(searchTerm.toLowerCase()))
              )
              .map(task => (
                <div key={task.id} className='card-wrapper' onClick={() => onTaskClick(task)}>
                  <div className="task-title">{task.title}</div>
                  <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                    <RenderState state={task.state} />
                    <div>
                      <button className="update-btn-small" onClick={(e) => { e.stopPropagation(); onEditClick(task); }}>✎</button>
                      <button className="delete-btn-small" onClick={(e) => { e.stopPropagation(); onDeleteClick(task.id); }}>✖</button>
                    </div>
                  </div>
                </div>
              ))}
          </div>
        ))}
      </div>
    </div>
  );
}