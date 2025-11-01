terraform {
  backend "s3" {
    bucket       = "demo-s3-test-2025"
    key          = "terraform.tfstate"
    region       = "eu-central-1"
    use_lockfile = true
    encrypt      = true
  }
}
