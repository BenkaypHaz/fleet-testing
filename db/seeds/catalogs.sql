-------------------------------------------
---INSERT IN TO TABLES
-------------------------------------------
INSERT INTO shipment.shipment_freight_status (id, name, is_active) VALUES
(1, 'In Transit', true),
(2, 'Stopped', true),
(3, 'Delivered', true),
(4, 'Finished', false);

INSERT INTO shipment.shipment_freight_type (id, name, is_active) VALUES
(1, 'Flete de patio', true),
(3, 'Flete Foráneo', true),
(2, 'Flete Internacional', true)

INSERT INTO shipment.freight_product_type (id, name, quality, is_active) VALUES
(1, 'Material de Construcción', 800, true),
(2, 'Repuestos Industriales', 300, true),
(3, 'Electrodomésticos', 450, false);

-------------------------------------------
---INSERT IN TO TABLES
-------------------------------------------
INSERT INTO accounting.accounting_fuel_order_type (id, serial_code, name, is_active) VALUES
(1, 'D', 'Diesel', true),
(2, 'S', 'Súper', true),
(3, 'R', 'Regular', true);

INSERT INTO accounting.accounting_expense_type (id, name, is_active) VALUES
(1, 'Hospedaje', true),
(2, 'Alimentación', true);

