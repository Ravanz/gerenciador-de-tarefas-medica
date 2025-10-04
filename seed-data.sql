USE vSaudeDb;
GO

-- Inserir tarefas de exemplo
INSERT INTO Tarefas (Titulo, Descricao, Prioridade, Categoria, Status, DataCriacao, IsDeleted)
VALUES 
    ('Consulta Dr. Silva', 'Revisão de exames laboratoriais - Paciente João', 1, 1, 0, GETUTCDATE(), 0),
    ('Cirurgia de emergência', 'Apendicite aguda - Paciente Maria - Sala 3', 3, 2, 1, GETUTCDATE(), 0),
    ('Análise de raio-X', 'Verificar fratura no braço direito - Paciente Pedro', 1, 3, 0, GETUTCDATE(), 0),
    ('Aplicar medicação', 'Antibiótico 500mg - Paciente Carlos', 2, 4, 0, GETUTCDATE(), 0),
    ('Consulta de rotina', 'Check-up anual - Paciente Ana', 0, 1, 2, GETUTCDATE(), 0),
    ('Exame de sangue', 'Hemograma completo - Paciente Roberto', 1, 3, 1, GETUTCDATE(), 0),
    ('Cirurgia agendada', 'Cirurgia de catarata - Paciente Laura - 10h', 2, 2, 0, GETUTCDATE(), 0),
    ('Atendimento emergência', 'Crise convulsiva - Paciente Fernando', 3, 5, 1, GETUTCDATE(), 0),
    ('Internação programada', 'Quimioterapia - Paciente Julia - Leito 205', 2, 6, 0, GETUTCDATE(), 0),
    ('Prescrição médica', 'Receituário para hipertensão - Paciente Marcos', 0, 4, 2, GETUTCDATE(), 0);
GO

-- Verificar dados inseridos
SELECT COUNT(*) as TotalTarefas FROM Tarefas;
GO

SELECT * FROM Tarefas ORDER BY DataCriacao DESC;
GO

