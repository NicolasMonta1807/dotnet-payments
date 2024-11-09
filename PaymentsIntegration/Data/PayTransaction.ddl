CREATE TABLE pay_transaction
(
    id               VARCHAR(64) PRIMARY KEY,
    amount           DECIMAL(10, 2) NOT NULL,
    transaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_successful    BOOLEAN        NOT NULL,
    message          TEXT,
    last_digits      VARCHAR(128)   NOT NULL
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;
