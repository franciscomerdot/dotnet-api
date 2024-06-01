provider "aws" {
  region = "us-west-2"
}

resource "aws_db_instance" "postgres" {
  allocated_storage    = 20
  storage_type         = "gp2"
  engine               = "postgres"
  engine_version       = "12.5"
  instance_class       = "db.t3.micro"
  name                 = "dotdot"
  username             = "postgres"
  password             = "postgres"
  parameter_group_name = "default.postgres12"
  skip_final_snapshot  = true
  publicly_accessible  = true
}

output "db_endpoint" {
  value = aws_db_instance.postgres.endpoint
}
